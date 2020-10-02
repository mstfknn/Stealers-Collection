using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000176 RID: 374
	public class WmiInstanceClass
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00009510 File Offset: 0x00007710
		public WmiInstanceProperty[] Properties { get; }

		// Token: 0x17000306 RID: 774
		public WmiInstanceProperty this[string propertyName]
		{
			get
			{
				foreach (WmiInstanceProperty wmiInstanceProperty in this.Properties)
				{
					if (wmiInstanceProperty.Name == propertyName)
					{
						return wmiInstanceProperty;
					}
				}
				throw new IndexOutOfRangeException("The specified property '" + propertyName + "' does not exists.");
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00009518 File Offset: 0x00007718
		public WmiInstanceClass(ICollection<WmiInstanceProperty> properties)
		{
			this.Properties = new WmiInstanceProperty[properties.Count];
			properties.CopyTo(this.Properties, 0);
		}
	}
}
