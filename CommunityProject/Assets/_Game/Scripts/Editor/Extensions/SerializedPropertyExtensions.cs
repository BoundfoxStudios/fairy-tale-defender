using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace BoundfoxStudios.CommunityProject.Editor.Extensions
{
	public static class SerializedPropertyExtensions
	{
		public static T GetValue<T>(this SerializedProperty property)
		{
			object result = property.propertyType switch
			{
				SerializedPropertyType.Float => property.floatValue,
				SerializedPropertyType.Integer => property.intValue,
				_ => throw new NotImplementedException($"Property type {property.propertyType} is not implemented yet.")
			};

			;
			return (T)Convert.ChangeType(result, typeof(T));
		}

		public static void SetValue<T>(this SerializedProperty property, T value)
		{
			// Unfortunately, we can not get rid of boxing here. But it's an editor script, so... :)
			var valueAsObject = Convert.ChangeType(value, typeof(T));

			switch (property.propertyType)
			{
				case SerializedPropertyType.Float:
					property.floatValue = (float)valueAsObject;
					break;

				case SerializedPropertyType.Integer:
					property.intValue = (int)valueAsObject;
					break;

				default:
					throw new NotImplementedException($"Property type {property.propertyType} is not implemented yet.");
			}
		}

		private static IEnumerable<T> GetAttributes<T>(this SerializedProperty property, bool inherit)
			where T : Attribute
		{
			const BindingFlags bindingFlags = (BindingFlags)(-1);

			var targetType = property.serializedObject.targetObject.GetType();

			while (targetType is not null)
			{
				var pathSegments = property.propertyPath.Split('.');

				foreach (var pathSegment in pathSegments)
				{
					var fieldInfo = targetType.GetField(pathSegment, bindingFlags);

					if (fieldInfo is not null)
					{
						return fieldInfo.GetCustomAttributes<T>(inherit);
					}

					var propertyInfo = targetType.GetProperty(pathSegment, bindingFlags);

					if (propertyInfo is not null)
					{
						return propertyInfo.GetCustomAttributes<T>(inherit);
					}
				}

				if (!inherit)
				{
					break;
				}

				targetType = targetType.BaseType;
			}

			return Array.Empty<T>();
		}

		/// <summary>
		/// Returns the first attribute of type <typeparamref name="T"/>.
		/// </summary>
		public static bool TryGetAttribute<T>(this SerializedProperty property, out T attribute, bool inherit = true)
			where T : Attribute
		{
			attribute = GetAttributes<T>(property, inherit).FirstOrDefault();
			return attribute is not null;
		}
	}
}
