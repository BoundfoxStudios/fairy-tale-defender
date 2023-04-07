using System;
using UnityEditor;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Infrastructure.ScriptableObjects
{
	/// <summary>
	/// Base class for ScriptableObjects to have a specific identity.
	/// See <see cref="ScriptableObjectIdentity"/> for more information.
	/// </summary>
	public abstract class IdentifiableSO : ScriptableObject, IEquatable<IdentifiableSO>
	{
		[field: SerializeField]
		// ReSharper disable once InconsistentNaming
		private string _guid { get; set; } = string.Empty;

		private ScriptableObjectIdentity? _identity;
		public ScriptableObjectIdentity Identity => _identity ??= new() { Guid = _guid };

		protected virtual void OnValidate()
		{
#if UNITY_EDITOR
			var path = AssetDatabase.GetAssetPath(this);
			_guid = AssetDatabase.AssetPathToGUID(path);
#endif
		}

		// ReSharper disable once MergeConditionalExpression
		// Don't merge it, because "other" is a UnityEngine.Object and will suppress the internal UnityEngine.Object check.
		public bool Equals(IdentifiableSO? other) => Identity.Equals(other is null ? null : other.Identity);

		public static implicit operator ScriptableObjectIdentity(IdentifiableSO identifiable) => identifiable.Identity;
	}
}
