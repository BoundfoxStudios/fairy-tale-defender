using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	public static class Guard
	{
		[Conditional("UNITY_ASSERTIONS")]
		public static void AgainstNull<T>(Expression<Func<T>> expression, Object? unityObject = null)
		{
			if (expression.Body is not MemberExpression member)
			{
				throw new ArgumentException($"Expression {expression} refers to a method, not a property or field");
			}

			var method = expression.Compile();
			var @object = method();

			Debug.Assert(@object is not null, $"{member.Member.Name} is null", unityObject);
		}
	}
}
