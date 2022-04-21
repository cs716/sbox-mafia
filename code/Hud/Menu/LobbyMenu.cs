using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System.Collections.Generic;
using System.Linq;
using TerryTrials.Hud.Components;
using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.Hud.Menu;

[UseTemplate]
public partial class LobbyMenu : Panel
{
	Label ReadyStatus { get; set; }
	Panel ReadyPanel { get; set; }
	public static bool IsReady
	{
		get => ((MafiaPlayer)Local.Pawn).IsReady;
	}

	public LobbyMenu()
	{
		GlyphIcon JumpButton = new (InputButton.Jump, InputGlyphSize.Small);
		ReadyPanel.AddChild( JumpButton );
	}

	public override void Tick()
	{
		SetClass( "hide", Game.Instance.GameState is not LobbyState );
		if (!HasClass("hide"))
		{
			foreach ( var player in Entity.All.OfType<MafiaPlayer>())
			{
				NameTagPanel panel;
				bool getPanel = Game.Instance.NameTagHandler.NameTags.TryGetValue( player, out panel );
				if ( getPanel )
				{
					panel.StatusLabel.Text = player.IsReady ? "Ready" : "Not Ready";
					panel.Classes = player.IsReady ? "green" : "red";
				}


			}
			ReadyStatus.Text = IsReady ? "Ready" : "Not Ready";
			ReadyStatus.SetClass( "ready", IsReady );
		}
		base.Tick();
	}

	[Event.BuildInput]
	public void ProcessBuildInput( InputBuilder inputBuilder )
	{
		Assert.NotNull( inputBuilder );
		if (!HasClass("hide"))
		{
			if (inputBuilder.Pressed(InputButton.Jump))
			{
				MafiaPlayer.ToggleReadyState( !IsReady );
			}
		}
	}
}
