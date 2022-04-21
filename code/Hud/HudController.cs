using Sandbox;
using Sandbox.UI;
using TerryTrials.Hud.Menu;

namespace TerryTrials.Hud;

[Library]
public partial class HudController : HudEntity<RootPanel>
{
	public HudController()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/Hud/Globals.scss" );
		RootPanel.AddChild<LobbyMenu>();
	}
}
