using Sandbox;
using Sandbox.UI;
using TerryTrials.Hud.Menu;
using TerryTrials.Hud.Overlay;

namespace TerryTrials.Hud;

[Library]
public partial class HudController : HudEntity<RootPanel>
{
	public BlackOverlay BlackOverlay;
	public HudController()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/Hud/Globals.scss" );
		RootPanel.AddChild<LobbyMenu>();
		RootPanel.AddChild<RoleAssignedMenu>();
		BlackOverlay = RootPanel.AddChild<BlackOverlay>();
	}
}
