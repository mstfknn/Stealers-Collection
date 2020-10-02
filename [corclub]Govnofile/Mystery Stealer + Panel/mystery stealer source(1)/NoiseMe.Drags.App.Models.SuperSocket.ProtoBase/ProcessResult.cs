using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public struct ProcessResult
	{
		public ProcessState State
		{
			get;
			private set;
		}

		public string Message
		{
			get;
			private set;
		}

		public IList<IPackageInfo> Packages
		{
			get;
			private set;
		}

		public static ProcessResult Create(ProcessState state)
		{
			ProcessResult result = default(ProcessResult);
			result.State = state;
			return result;
		}

		public static ProcessResult Create(ProcessState state, string message)
		{
			return Create(state, message, null);
		}

		public static ProcessResult Create(ProcessState state, IList<IPackageInfo> packages)
		{
			return Create(state, string.Empty, packages);
		}

		public static ProcessResult Create(ProcessState state, string message, IList<IPackageInfo> packages)
		{
			ProcessResult result = default(ProcessResult);
			result.State = state;
			result.Message = message;
			result.Packages = packages;
			return result;
		}
	}
}
