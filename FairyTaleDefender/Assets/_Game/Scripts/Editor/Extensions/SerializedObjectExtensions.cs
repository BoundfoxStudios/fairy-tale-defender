using UnityEditor;

namespace BoundfoxStudios.FairyTaleDefender.Editor.Extensions
{
	public static class SerializedObjectExtensions
	{
		/// <summary>
		/// Finds a <see cref="SerializedProperty"/> C# property with a [field: SerializedField] attribute,
		/// because Unity's <see cref="SerializedObject.FindProperty"/> does not work with C# properties.
		/// </summary>
		public static SerializedProperty FindRealProperty(this SerializedObject serializedObject, string name)
		{
			// This is the serialized name of a C# property.
			var realName = $"<{name}>k__BackingField";
			return serializedObject.FindProperty(realName);
		}
	}
}
