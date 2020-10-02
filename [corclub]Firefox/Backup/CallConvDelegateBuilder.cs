using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace MozillaPasswords
{
    /// <summary>
    /// Cette classe permet de générer un délégué avec une convention d'appel particulière
    /// Ne marche PAS avec le framework 1.0/1.1
    /// </summary>
    public class CallConvDelegateBuilder
    {
        //cache des délégués créés (nécessaire dans la mesure où la génération de classe au runtime est un processus relativement long)
        private static Dictionary<string, Type> generated_delegates = new Dictionary<string, Type>();

        /// <summary>
        /// Renvoie le nom de la classe générée (sans le Delegate de fin)
        /// </summary>
        /// <param name="delegateMethodInfo">méthode dont on va générer un délégué</param>
        /// <param name="callConv">convention d'appel du délégué à générer</param>
        /// <returns></returns>
        private static string GetNewDelegateName(MethodInfo delegateMethodInfo, CallingConvention callConv)
        {
            return delegateMethodInfo.Name + callConv;
        }

        /// <summary>
        /// Génère un Type dérivé de MulticastDelegate suivant la signature d'une méthode
        /// </summary>
        /// <param name="delegateMethodInfo">information sur la méthode qui servira à générer le délégué</param>
        /// <param name="callConv">convention d'appel du délégué</param>
        /// <param name="delegName">nom du délégué à générer</param>
        /// <returns></returns>
        private static Type CreateDelegate(MethodInfo delegateMethodInfo, CallingConvention callConv,string delegName)
        {
            //Un délégué est une classe :
            //-> dérivant de MulticastDelegate
            //-> ayant un constructeur prenant deux paramètres (la méthode à appeler et l'objet sur lequel appeler la méthode)
            //-> une méthode Invoke pour appeler la méthode de manière synchrone
            //-> une paire de méthodes BeginInvoke/EndInvoke pour appeler la méthode de manière asynchrone
            //-> la signature des trois méthodes précédentes dépend de la signature de la méthode à appeler
            //   cette classe est donc généré à la compilation par rapport à la signature déclarée avec le mot clé "delegate"

            //génère un nom pour l'assembly qui contiendra le type du délégué
            AssemblyName asm_name = new AssemblyName(delegName);
            //nom du NetModule  qui contiendra le type
            string moduleName = asm_name.Name + "Module.dll";
            //construit un assembly pour enregistrement et exécution pour contenir le type du délégué dans l'AppDomain courant
            AssemblyBuilder asm_builder = AppDomain.CurrentDomain.DefineDynamicAssembly(asm_name, AssemblyBuilderAccess.RunAndSave);
            //construit un module
            ModuleBuilder mod_builder = asm_builder.DefineDynamicModule(moduleName, moduleName, true);
            //construit un type dérivé de MulticastDelegate pour le délégué
            TypeBuilder type_builder = mod_builder.DefineType(
                asm_name.Name + "Delegate",
                TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.AutoClass | TypeAttributes.Sealed,
                typeof(System.MulticastDelegate));

            //type de retour de la méthode
            Type method_return = delegateMethodInfo.ReturnType;
            //modopt et modreq du type de retour des méthodes du délégués (permet de spécifier la convention d'appel)
            Type[] method_return_modreq = null;
            Type[] method_return_modopt = null;

            //ajout du modopt correspondant à la convention d'appel
            switch (callConv)
            {
                case CallingConvention.Cdecl:
                    method_return_modopt = new Type[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) };
                    break;
                case CallingConvention.FastCall:
                    method_return_modopt = new Type[] { typeof(System.Runtime.CompilerServices.CallConvFastcall) };
                    break;
                case CallingConvention.StdCall:
                    method_return_modopt = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) };
                    break;
                case CallingConvention.ThisCall:
                    method_return_modopt = new Type[] { typeof(System.Runtime.CompilerServices.CallConvThiscall) };
                    break;
                case CallingConvention.Winapi:
                    method_return_modopt = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) };
                    break;
                default:
                    break;
            }

            //récupère la liste des paramètres de la méthode qui sert de base délégué
            //ainsi que leurs éventuels modopt et modreq
            List<Type> method_parameters = new List<Type>();
            List<Type[]> method_parameters_modreq = new List<Type[]>();
            List<Type[]> method_parameters_modopt = new List<Type[]>();
            foreach (ParameterInfo par in delegateMethodInfo.GetParameters())
            {
                method_parameters.Add(par.ParameterType);
                method_parameters_modreq.Add(par.GetRequiredCustomModifiers());
                method_parameters_modopt.Add(par.GetOptionalCustomModifiers());
            }

            //=======================================================================================================================
            //génère le constructeur du délégué : .ctor(object objThis,IntPtr method_pointer)
            //prend deux paramètres : 
            //-> objThis : l'instance d'objet sur lequel appeler la méthode (null si static)
            //-> method_pointer : le pointeur vers la méthode à appeler
            ConstructorBuilder construct_builder = type_builder.DefineConstructor(
                //constructeur public
                //ne cachant que le constructeur de la classe de base avec la même signature
                //avec un nom spécial vu qu'il s'agit d'un constructeur 
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName, 
                CallingConventions.Standard, 
                new Type[] { typeof(object), typeof(IntPtr) });
            //pas de corps : généré au runtime par le CLR
            construct_builder.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
            //=======================================================================================================================

            //=======================================================================================================================
            //génére la méthode EndInvoke du délégué (méthode permettant de récupérer le résultat d'un appel asynchrone
            //type_retour_méthode EndInvoke(IAsyncResult  status)
            MethodBuilder end_invoke = type_builder.DefineMethod(
                "EndInvoke", 
                //méthode publique, ne cachant qu'une méthode de la classe de base ayant une signature identique
                //méthode ne participant pas au polymorphisme depuis la classe de base (appartient à cette classe seulement "new")
                //(normal, vu que une méthode avec le même nom peut exister dans la classe de base mais pas avec la même signature)
                //mais pouvant être overridée dans une sous classe
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, 
                CallingConventions.Standard);
            //définit un paramètre de type IAsyncResult contenant le status de l'appel asynchrone
            end_invoke.SetParameters(typeof(System.IAsyncResult));
            //renvoyant le résultat de la méthode appelée
            end_invoke.SetReturnType(method_return);
            //pas de corps : généré au runtime par le CLR
            end_invoke.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
            //=======================================================================================================================

            //=======================================================================================================================
            //récupère le constructeur "internal" de la classe MarshalAsAttribute 
            //(permettant d'initialiser l'attribut avec toutes ses propriétés)
            ConstructorInfo marshalas_ctor = typeof(MarshalAsAttribute).GetConstructor(
                //pas public puisqu'internal
                BindingFlags.NonPublic | BindingFlags.Instance,null,
                //constructeur avec un paramètre pour chaque propriété
                new Type[] { typeof(UnmanagedType) , typeof(VarEnum) , typeof(Type) , 
                             typeof(UnmanagedType) , typeof(short) , typeof(int) , typeof(string) , 
                             typeof(Type) , typeof(string) , typeof(int) },null);

            //génère le méthode Invoke du délégué (méthode permettant d'appeler de manière synchrone la méthode contenue dans le délégué)
            //type_retour_méthode Invoke(paramètres_méthode)
            MethodBuilder invoke = type_builder.DefineMethod(
                "Invoke",
                //méthode publique, ne cachant qu'une méthode de la classe de base ayant une signature identique
                //méthode ne participant pas au polymorphisme depuis la classe de base (appartient à cette classe seulement "new")
                //(normal, vu que une méthode avec le même nom peut exister dans la classe de base mais pas avec la même signature)
                //mais pouvant être overridée dans une sous classe
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, 
                CallingConventions.Standard,
                //type de retour
                method_return,
                //éventuel modreq de la méthode
                method_return_modreq,
                //modopt indiquant la convention d'appel de la méthode
                method_return_modopt,
                //les paramètres
                method_parameters.ToArray(),
                //les modreq et modopt des paramètres
                method_parameters_modreq.ToArray(),
                method_parameters_modopt.ToArray());
            //pour chaque paramètre de la méthode
            foreach (ParameterInfo par in delegateMethodInfo.GetParameters())
            {
                //lui ajouter ses attributs (in, ref, out...) et son nom
                ParameterBuilder b = invoke.DefineParameter(par.Position+1, par.Attributes, par.Name);
                //pour chaque attribut de Marshalling du paramètre
                foreach (MarshalAsAttribute mar in par.GetCustomAttributes(typeof(MarshalAsAttribute),false))
                {
                    //ajouter au paramètre l'attribut MarshalAs correspondant
                    //nécessite d'avoir un constructeur de l'attribut et les valeurs des paramètres de ce constructeur
                    b.SetCustomAttribute(new CustomAttributeBuilder(marshalas_ctor, 
                        new object[] { 
                            mar.Value, mar.SafeArraySubType, mar.SafeArrayUserDefinedSubType, 
                            mar.ArraySubType, mar.SizeParamIndex, mar.SizeConst, 
                            mar.MarshalType, mar.MarshalTypeRef, mar.MarshalCookie, mar.IidParameterIndex
                        }));  
                }
            }
            //pas de corps : généré au runtime par le CLR
            invoke.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
            //=======================================================================================================================

            //=======================================================================================================================
            //génère le méthode BeginInvoke du délégué 
            //(méthode permettant d'appeler de manière asynchrone la méthode contenue dans le délégué)
            //(nécessite EndInvoke pour récupérer le résultat de l'exécution asynchrone)
            //type_retour_méthode BeginInvoke(paramètres_méthode,AsyncCallback callback,object arg)
            //(on peut passer une méthode dans le paramètre callback. 
            //cette méthode sera déclencher lorsque la méthode contenue dans le délégué renvoie sont résultat
            //elle (callback) recevra arg en paramètre

            //ajoute les deux dernières paramètres de BeginInvoke à la liste des paramètres de la méthode
            method_parameters.Add(typeof(System.AsyncCallback));
            method_parameters.Add(typeof(object));
            //ajoute le nombre de cases nécessaires au modopt et modreq des paramètres
            //sinon DefineMethod lance une exception
            if (method_parameters_modopt.Count > 0)
            {
                method_parameters_modopt.Add(null);
                method_parameters_modopt.Add(null);
            }
            if (method_parameters_modreq.Count > 0)
            {
                method_parameters_modreq.Add(null);
                method_parameters_modreq.Add(null);
            }

            MethodBuilder begin_invoke = type_builder.DefineMethod(
                "BeginInvoke",
                //méthode publique, ne cachant qu'une méthode de la classe de base ayant une signature identique
                //méthode ne participant pas au polymorphisme depuis la classe de base (appartient à cette classe seulement "new")
                //(normal, vu que une méthode avec le même nom peut exister dans la classe de base mais pas avec la même signature)
                //mais pouvant être overridée dans une sous classe
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual, 
                CallingConventions.Standard,
                //renvoie un résultat d'exécution asynchrone nécessaire à la récupération du retour de la méthode
                typeof(System.IAsyncResult),
                //éventuel modreq de la méthode
                method_return_modreq,
                //modopt indiquant la convention d'appel de la méthode
                method_return_modopt,
                //les paramètres
                method_parameters.ToArray(),
                //les modreq et modopt des paramètres
                method_parameters_modreq.ToArray(),
                method_parameters_modopt.ToArray());
            //pour chaque paramètre de la méthode
            foreach (ParameterInfo par in delegateMethodInfo.GetParameters())
            {
                //lui ajouter ses attributs (in, ref, out...) et son nom
                ParameterBuilder b = begin_invoke.DefineParameter(par.Position + 1, par.Attributes, par.Name);
                //pour chaque attribut de Marshalling du paramètre
                foreach (MarshalAsAttribute mar in par.GetCustomAttributes(typeof(MarshalAsAttribute), false))
                {
                    //ajouter au paramètre l'attribut MarshalAs correspondant
                    //nécessite d'avoir un constructeur de l'attribut et les valeurs des paramètres de ce constructeur
                    b.SetCustomAttribute(new CustomAttributeBuilder(marshalas_ctor,
                        new object[] { 
                            mar.Value, mar.SafeArraySubType, mar.SafeArrayUserDefinedSubType, 
                            mar.ArraySubType, mar.SizeParamIndex, mar.SizeConst, 
                            mar.MarshalType, mar.MarshalTypeRef, mar.MarshalCookie, mar.IidParameterIndex
                        }));
                }
            }
            //ajoute les attributs et noms des deux derniers paramètres
            begin_invoke.DefineParameter(method_parameters.Count - 1, ParameterAttributes.None, "callback");
            begin_invoke.DefineParameter(method_parameters.Count, ParameterAttributes.None, "o");
            //pas de corps : généré au runtime par le CLR
            begin_invoke.SetImplementationFlags(MethodImplAttributes.Managed | MethodImplAttributes.Runtime);
            //=======================================================================================================================

            //génère le Type ainsi défini
            Type t = type_builder.CreateType();

            //Debug : enregistrer l'assembly générer
            //asm_builder.Save(moduleName);

            return t;
        }

        /// <summary>
        /// Crée une instance d'un délégué chargé d'appeler la méthode décrite par delegateMethodInfo sur objThis
        /// ou la méthode static décrite par delegateMethodInfo
        /// suivant une convention d'appel particulière
        /// </summary>
        /// <param name="delegateMethodInfo">information sur la méthode qui servira à générer le délégué et qui sera appelée par celui-ci</param>
        /// <param name="callConv">convention d'appel du délégué</param>
        /// <param name="objThis">instance de l'objet qui contient la méthode à appeler par le délégué créé ou null si la méthode est statique</param>
        /// <returns>renvoie une instance du délégué qui appelera la méthode de l'objet passé</returns>
        public static MulticastDelegate CreateDelegateInstance(MethodInfo delegateMethodInfo, CallingConvention callConv,object objThis)
        {
            //génère le nom du délégué à générer
            string delegName = GetNewDelegateName(delegateMethodInfo,callConv);
            Type delegate_type;
            //si on l'a déjà généré
            if (generated_delegates.ContainsKey(delegName))
                //on récupère
                delegate_type = generated_delegates[delegName];
            else
            {
                //sinon, on le crée et on l'ajoute à la liste
                delegate_type = CreateDelegate(delegateMethodInfo, callConv, delegName);
                generated_delegates.Add(delegName,delegate_type);
            }
            //crée une instance du délégué, initialisé avec l'objet qui contient la méthode (ou null si static) et le pointeur vers la méthode à appeler
            //rappel : une méthode d'instance n'est jamais qu'une simple fonction qui prend en premier (ou dernier) paramètre, un pointeur vers une instance de la classe
            return (MulticastDelegate)Activator.CreateInstance(delegate_type, new object[] { objThis, delegateMethodInfo.MethodHandle.GetFunctionPointer() });
        }

        /// <summary>
        /// Crée une instance d'un délégué chargé d'appeler la méthode objThis."methodName" 
        /// ou la méthode static methodContainer."methodName"
        /// suivant une convention d'appel particulière
        /// </summary>
        /// <param name="methodContainer">Type contenant la méthode methodName</param>
        /// <param name="methodName">nom de la méthode dont on veut créer un délégué</param>
        /// <param name="callConv">convention d'appel du délégué</param>
        /// <param name="objThis">instance de l'objet qui contient la méthode à appeler par le délégué créé ou null si la méthode est statique</param>
        /// <returns>renvoie une instance du délégué qui appelera la méthode de l'objet passé</returns>
        public static MulticastDelegate CreateDelegateInstance(Type methodContainer, string methodName, CallingConvention callConv, object objThis)
        {
            //récupère les infos sur la méthode
            MethodInfo deleg = methodContainer.GetMethod(
                methodName, 
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                );
            return CreateDelegateInstance(deleg, callConv, objThis);
        }
    }
}
