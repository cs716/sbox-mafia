using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.Hud;

public partial class NameTagPanel : WorldPanel
{
	private readonly MafiaPlayer Player;
	private readonly Label NameLabel;
	public readonly Label StatusLabel;
	public readonly Label RoleLabel;

	public NameTagPanel(MafiaPlayer player)
	{
		this.Player = player;
		NameLabel = Add.Label( "Loading ..", "NameTag" );
		StatusLabel = Add.Label( string.Empty, "StatusLabel" );
		RoleLabel = Add.Label( string.Empty, "RoleLabel" );
		StyleSheet.Load( "/Hud/World/Player/NameTagPanel.scss" );
	}

	public override void Tick()
	{
		if ( !Player.IsValid() || Player is null || !Player.IsAlive)
		{
			Delete( true );
			return;
		}

		NameLabel.Text = Player.Client.Name;
		var playerPosition = CurrentView.Position;
		playerPosition.z = Position.z;

		var targetRotation = Rotation.LookAt( playerPosition - Position );
		var transform = new Transform( Player.Position + Vector3.Up * 100, Player.Rotation );
		transform.Rotation = Rotation.Lerp( transform.Rotation, targetRotation, 1f );

		Transform = transform;

		if (Game.Instance.GameState is NightState || Game.Instance.GameState is DayState)
		{
			LocalKnown playerInfo = LocalKnowns.Instance.GetKnown( Player );
			if ( playerInfo.KnownType != KnownType.UNKNOWN )
			{
				RoleLabel.SetClass( "green", playerInfo.KnownType == KnownType.ALLY );
				RoleLabel.SetClass( "red", playerInfo.KnownType == KnownType.ENEMY );
				RoleLabel.SetClass( "blue", playerInfo.KnownType == KnownType.NEUTRAL );
			}

			RoleLabel.SetText( playerInfo.TeamName.Length > 1 ? playerInfo.TeamName : string.Empty );
		} else if (Game.Instance.GameState is not LobbyState && StatusLabel.Text.Length > 0)
		{
			StatusLabel.SetText( string.Empty );
		}
		StatusLabel.SetClass( "hide", StatusLabel.Text == string.Empty );
		base.Tick();
	}
}


