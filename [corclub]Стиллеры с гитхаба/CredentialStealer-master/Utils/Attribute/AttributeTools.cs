using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CredentialStealer.Utils.Attribute
{
    public class Tools
    {
        /// <summary>
        /// recupère attribut Description de l'enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            string stringVal = "No description";
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                stringVal = attributes[0].Description;
            return stringVal;
        }

        /// <summary>
        /// recupère attribut KeyAttribute de l'enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumKeyAttribute(Enum value)
        {
            string stringVal = value.ToString();
            FieldInfo fi = value.GetType().GetField(value.ToString());
            KeyAttribute[] attributes = (KeyAttribute[])fi.GetCustomAttributes(typeof(KeyAttribute), false);
            if (attributes != null && attributes.Length > 0)
                stringVal = attributes[0].Key;
            return stringVal;
        }

		
		/// <summary>
        /// recupère attribut de type ICustomAttribute de l'enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static U GetEnumAttributeValue<T, U>(Enum value) where T : ICustomAttribute<U>
        {
            U stringVal = default(U);
            FieldInfo fi = value.GetType().GetField(value.ToString());
            T[] attributes = fi.GetCustomAttributes(typeof(T), false).Cast<T>().ToArray();
            if (attributes != null && attributes.Length > 0)
                stringVal = attributes[0].Key;
            return stringVal;
        }
		
        /// <summary>
        /// recupère l'enum à partir de la valeur du KeyAttribute
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static U GetEnumFromKey<U>(string value)
        {
            U result = default(U);
            foreach (Enum item in Enum.GetValues(typeof(U)))
            {
                if (string.Compare(Tools.GetEnumKeyAttribute(item), value, true) == 0)
                {
                    result = (U)Enum.Parse(typeof(U), item.ToString());
                    break;
                }
            }
            return result;
        }
    }
}
