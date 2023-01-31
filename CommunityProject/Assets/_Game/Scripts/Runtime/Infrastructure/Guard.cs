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
		public static void AgainstNull<T>(Expression<Func<T>> expression, Object? unityObject = null,
			bool suppressMessageInPrefabStage = false)
		{
			if (expression.Body is not MemberExpression member)
			{
				throw new ArgumentException($"Expression {expression} refers to a method, not a property or field");
			}

			var method = expression.Compile();
			var @object = method();

			// ReSharper disable once RedundantAssignment
			// Due to conditional code below
			var showAssert = true;

#if UNITY_EDITOR
			showAssert = (
				             !suppressMessageInPrefabStage
				             && UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() is not null
			             )
			             && !UnityEditor.PrefabUtility.IsPartOfPrefabAsset(unityObject);
#endif

			if (showAssert)
			{
				var name = GetName(member);
				Debug.Assert(@object is not null, $"{name} is null", unityObject);
			}
		}

		private static string GetName(MemberExpression? memberExpression)
		{
			var name = string.Empty;

			while (memberExpression is { } innerMemberExpression)
			{
				name = innerMemberExpression.Member.Name + ".";
				memberExpression = innerMemberExpression.Expression as MemberExpression;
			}

			return name.TrimEnd('.');
		}
	}
}
