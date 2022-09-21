using System;

namespace BoundfoxStudios.CommunityProject.Infrastructure
{
	/// <summary>
	/// This class can be used for ScriptableObjects that should have an identity.
	/// The identity is useful for saving references to disk and loading them back at runtime.
	/// </summary>
	[Serializable]
	public class ScriptableObjectIdentity : IEquatable<ScriptableObjectIdentity>
	{
		public string Guid;

		public bool Equals(ScriptableObjectIdentity other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return string.Equals(Guid, other.Guid, StringComparison.InvariantCultureIgnoreCase);
		}

		public override bool Equals(object obj) => Equals(obj as ScriptableObjectIdentity);

		public static bool operator ==(ScriptableObjectIdentity lhs, ScriptableObjectIdentity rhs)
		{
			if (ReferenceEquals(lhs, rhs))
			{
				return true;
			}

			if (ReferenceEquals(lhs, null))
			{
				return false;
			}

			return lhs.Equals(rhs);
		}

		public static bool operator !=(ScriptableObjectIdentity lhs, ScriptableObjectIdentity rhs) => !(lhs == rhs);

		// ReSharper disable once NonReadonlyMemberInGetHashCode
		public override int GetHashCode() => StringComparer.InvariantCultureIgnoreCase.GetHashCode(Guid);
	}
}
