using System;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf.Meta;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x02000158 RID: 344
	public static class ProtoHelper
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x000225A8 File Offset: 0x000207A8
		public static void PopulateTypes(Type t)
		{
			using (IEnumerator enumerator = RuntimeTypeModel.Default.GetTypes().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MetaType metaType;
					if ((metaType = (enumerator.Current as MetaType)) != null && metaType.Type == t)
					{
						return;
					}
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
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				foreach (object obj in ((@default != null) ? @default.GetTypes() : null))
				{
					MetaType metaType3 = (MetaType)obj;
					if (metaType3.Name.Equals(type.Name))
					{
						flag = true;
						metaType2 = metaType3;
						break;
					}
				}
				if (!flag)
				{
					RuntimeTypeModel default2 = RuntimeTypeModel.Default;
					metaType2 = ((default2 != null) ? default2.Add(type, true) : null);
				}
				metaType2.AddSubType((metaType2.GetSubtypes().Length + 1) * 100, list[i + 1]);
			}
		}
	}
}
