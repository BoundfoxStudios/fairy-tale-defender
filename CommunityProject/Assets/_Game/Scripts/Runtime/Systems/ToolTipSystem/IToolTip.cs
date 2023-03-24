using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoundfoxStudios.CommunityProject
{
	public interface IToolTip
	{
		Vector3 Position { get; }
	}
	public interface ITextToolTip : IToolTip
	{
		string ToolTipText { get; }
	}

}
