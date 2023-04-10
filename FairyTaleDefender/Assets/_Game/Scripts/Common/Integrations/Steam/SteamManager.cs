using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Common.Integrations.Steam
{
	public class SteamManager : MonoBehaviour
	{
		public virtual void Initialize()
		{
			Debug.Log("Steam integration has been turned off. To enable go to Fairy Tale Defender -> Windows -> Steam integration");
		}
	}
}
