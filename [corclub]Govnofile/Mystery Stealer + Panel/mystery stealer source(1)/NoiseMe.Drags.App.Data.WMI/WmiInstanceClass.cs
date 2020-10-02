using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	public class WmiInstanceClass
	{
		public WmiInstanceProperty[] Properties
		{
			get;
		}

		public WmiInstanceProperty this[string propertyName]
		{
			get
			{
				WmiInstanceProperty[] properties = Properties;
				foreach (WmiInstanceProperty wmiInstanceProperty in properties)
				{
					if (wmiInstanceProperty.Name == propertyName)
					{
						return wmiInstanceProperty;
					}
				}
				throw new IndexOutOfRangeException("The specified property '" + propertyName + "' does not exists.");
			}
		}

		public WmiInstanceClass(ICollection<WmiInstanceProperty> properties)
		{
			Properties = new WmiInstanceProperty[properties.Count];
			properties.CopyTo(Properties, 0);
		}
	}
}
