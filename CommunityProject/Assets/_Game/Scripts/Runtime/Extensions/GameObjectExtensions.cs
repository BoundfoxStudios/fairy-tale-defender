using System.Runtime.CompilerServices;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject.Extensions
{
	public static class GameObjectExtensions
	{
		public static void Deactivate(this GameObject gameObject)
		{
			if (gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}

		public static void Activate(this GameObject gameObject)
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}
	}
}
