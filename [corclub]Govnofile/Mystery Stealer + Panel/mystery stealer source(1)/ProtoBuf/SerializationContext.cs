using System;

namespace ProtoBuf
{
	public sealed class SerializationContext
	{
		private bool frozen;

		private object context;

		private static readonly SerializationContext @default;

		public object Context
		{
			get
			{
				return context;
			}
			set
			{
				if (context != value)
				{
					ThrowIfFrozen();
					context = value;
				}
			}
		}

		internal static SerializationContext Default => @default;

		internal void Freeze()
		{
			frozen = true;
		}

		private void ThrowIfFrozen()
		{
			if (frozen)
			{
				throw new InvalidOperationException("The serialization-context cannot be changed once it is in use");
			}
		}

		static SerializationContext()
		{
			@default = new SerializationContext();
			@default.Freeze();
		}
	}
}
