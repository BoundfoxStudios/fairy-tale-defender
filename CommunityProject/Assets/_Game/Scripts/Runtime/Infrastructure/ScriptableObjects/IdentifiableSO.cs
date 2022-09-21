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
		[SerializeField]
		[HideInInspector]
		// ReSharper disable once InconsistentNaming
		private string _guid;

		private ScriptableObjectIdentity _identity;
		public ScriptableObjectIdentity Identity => _identity ??= new() { Guid = _guid };

		protected virtual void OnValidate()
		{
#if UNITY_EDITOR
			var path = AssetDatabase.GetAssetPath(this);
			_guid = AssetDatabase.AssetPathToGUID(path);
#endif
		}

		public bool Equals(IdentifiableSO other) => Identity.Equals(other == null ? null : other.Identity);

		public static implicit operator ScriptableObjectIdentity(IdentifiableSO identifiable) => identifiable.Identity;
	}
}
