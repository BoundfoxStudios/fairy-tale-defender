using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Extensions
{
	public static class SerializedObjectExtensions
	{
		public static SerializedProperty FindRealProperty(this SerializedObject serializedObject, string name)
		{
			var realName = $"<{name}>k__BackingField";
			return serializedObject.FindProperty(realName);
		}
	}
}
