using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	public static class Guard
	{
		/// <summary>
		/// Use this method to check a property or field against null.
		/// Best suitable in OnValidate to check serialized fields.
		/// Take care, this method only works in editor and won't be compiled to the player build.
		/// </summary>
		[Conditional("UNITY_ASSERTIONS")]
		public static void AgainstNull<T>(Expression<Func<T>> expression, Object? unityObject = null)
		{
			if (expression.Body is not MemberExpression member)
			{
				throw new ArgumentException($"Expression {expression} refers to a method, not a property or field");
			}

			var method = expression.Compile();
			var @object = method();

			// ReSharper disable once RedundantAssignment
			// Due to conditional code below
			var isPrefab = false;

#if UNITY_EDITOR
			isPrefab = UnityEditor.PrefabUtility.IsPartOfPrefabAsset(unityObject);
#endif

			if (!isPrefab)
			{
				Debug.Assert(@object is not null, $"{member.Member.Name} is null", unityObject);
			}
		}
	}
}
