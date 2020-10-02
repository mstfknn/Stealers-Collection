#region

using System;
using System.Collections.Generic;
using System.IO;
using Confuser.Renamer;
using dnlib.DotNet;
using Microsoft.VisualBasic;

#endregion

namespace KoiVM.RT.Mutation
{
    public class Renamer
    {
        private readonly Dictionary<string, string> nameMap = new Dictionary<string, string>();
        private readonly int next;

        public Renamer(int seed) => this.next = seed;

        private string ToString(int id) => id.ToString("x");

        private string NewName(string name)
        {
            if (!this.nameMap.TryGetValue(name, out string newName))
            {
                this.nameMap[name] = newName = NameService.RandomNameStatic();
            }
            return newName;
        } //you need to change this method, i use NameService so i don't have to do the renamer method again

        public void Process(ModuleDef module)
        {
            foreach(TypeDef type in module.GetTypes())
            {
                if(!type.IsPublic)
                {
                    type.Namespace = this.NewName(type.Namespace); // you can add this if u want to get namespace seperation on koivm classes
                    type.Name = this.NewName(type.Name);
                }
                foreach(GenericParam genParam in type.GenericParameters)
                    genParam.Name = "";

                bool isDelegate = type.BaseType != null &&
                                 (type.BaseType.FullName == "System.Delegate" ||
                                  type.BaseType.FullName == "System.MulticastDelegate");

                foreach(MethodDef method in type.Methods)
                {
                    if(method.HasBody)
                        foreach(dnlib.DotNet.Emit.Instruction instr in method.Body.Instructions)
                        {
                            if (instr.Operand is MemberRef memberRef)
                            {
                                TypeDef typeDef = memberRef.DeclaringType.ResolveTypeDef();

                                if (memberRef.IsMethodRef && typeDef != null)
                                {
                                    MethodDef target = typeDef.ResolveMethod(memberRef);
                                    if (target != null && target.IsRuntimeSpecialName)
                                        typeDef = null;
                                }

                                if (typeDef != null && typeDef.Module == module)
                                    memberRef.Name = this.NewName(memberRef.Name);
                            }
                        }

                    foreach(Parameter arg in method.Parameters)
                        arg.Name = "";
                    if(method.IsRuntimeSpecialName || isDelegate || type.IsPublic)
                        continue;
                    method.Name = this.NewName(method.Name);
                    method.CustomAttributes.Clear();
                }
                for(int i = 0; i < type.Fields.Count; i++)
                {
                    FieldDef field = type.Fields[i];
                    if(field.IsLiteral)
                    {
                        type.Fields.RemoveAt(i--);
                        continue;
                    }
                    if(field.IsRuntimeSpecialName)
                        continue;
                    field.Name = this.NewName(field.Name);
                }
                type.Properties.Clear();
                type.Events.Clear();
                type.CustomAttributes.Clear();
            }
        }
    }
}