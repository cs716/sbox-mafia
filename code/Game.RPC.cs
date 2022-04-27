using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerryTrials.Hud.Menu;
using TerryTrials.Hud.Overlay;
using TerryTrials.Player;

namespace TerryTrials;
public partial class Game : Sandbox.Game
{

	[ClientRpc]
	public static void OverlayFadeIn( int time )
	{
		Local.Hud.ChildrenOfType<BlackOverlay>().FirstOrDefault().FadeIn( time );
	}

	[ClientRpc]
	public static void OverlayFadeOut( int time )
	{
		Local.Hud.ChildrenOfType<BlackOverlay>().FirstOrDefault().FadeOut( time );
	}

	[ClientRpc]
	public static void DisplayPlayerRoleToPlayer(string name, Color roleColor, string description, bool displayOnScreen)
	{
		var roleDisplay = Local.Hud.ChildrenOfType<RoleAssignedMenu>().FirstOrDefault();
		if (roleDisplay != null)
		{
			roleDisplay.Role.SetText(name);
			roleDisplay.Role.Style.FontColor = roleColor;
			roleDisplay.RoleDescription = description;
			if ( displayOnScreen )
				roleDisplay.Show = true;
		}
	}

	[ClientRpc]
	public static void HidePlayerRoleMenu()
	{
		var roleDisplay = Local.Hud.ChildrenOfType<RoleAssignedMenu>().FirstOrDefault();
		if ( roleDisplay != null )
		{
			roleDisplay.Show = false;
		}
	}
}
