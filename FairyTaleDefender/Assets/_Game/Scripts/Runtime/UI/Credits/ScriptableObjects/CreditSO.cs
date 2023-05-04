using BoundfoxStudios.FairyTaleDefender.Build.Contributors;
using BoundfoxStudios.FairyTaleDefender.Common;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.UI.Credits.ScriptableObjects
{
	[CreateAssetMenu(menuName = Constants.MenuNames.UI + "/Credit")]
	public class CreditSO : ScriptableObject
	{
		[field: SerializeField]
		public Contributor Contributor { get; private set; } = default!;
	}
}
