using ProtoBuf.Meta;
using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.Communication
{
	public static class ProtoHelper
	{
		public static void PopulateTypes(Type t)
		{
			foreach (object type2 in RuntimeTypeModel.Default.GetTypes())
			{
				MetaType metaType;
				if ((metaType = (type2 as MetaType)) != null && metaType.Type == t)
				{
					return;
				}
			}
			Type typeFromHandle = typeof(object);
			List<Type> list = new List<Type>();
			do
			{
				list.Insert(0, t);
				t = t.BaseType;
			}
			while (t != null && t != typeFromHandle);
			for (int i = 0; i < list.Count - 1; i++)
			{
				Type type = list[i];
				MetaType metaType2 = null;
				bool flag = false;
				foreach (MetaType item in RuntimeTypeModel.Default?.GetTypes())
				{
					if (item.Name.Equals(type.Name))
					{
						flag = true;
						metaType2 = item;
						break;
					}
				}
				if (!flag)
				{
					metaType2 = RuntimeTypeModel.Default?.Add(type, applyDefaultBehaviour: true);
				}
				metaType2.AddSubType((metaType2.GetSubtypes().Length + 1) * 100, list[i + 1]);
			}
		}
	}
}
