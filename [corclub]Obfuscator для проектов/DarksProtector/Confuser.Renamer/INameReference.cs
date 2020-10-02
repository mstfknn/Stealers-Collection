using System;
using System.Collections.Generic;
using System.Linq;
using Confuser.Core;

namespace Confuser.Renamer
{
    public interface INameReference {
		bool UpdateNameReference(ConfuserContext context, INameService service);

		bool ShouldCancelRename();
	}

	public interface INameReference<out T> : INameReference { }

    public static class ListExtensions
    {
        public static T PickRandom<T>(this List<T> enumerable)
        {
#pragma warning disable SCS0005 // Weak random generator
            int index = new Random().Next(0, enumerable.Count());
#pragma warning restore SCS0005 // Weak random generator
            return enumerable[index];
        }
    }
}