using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using TerryTrials.Player;

namespace TerryTrials.Hud;

public partial class NameTagPanel : WorldPanel
{
	private readonly MafiaPlayer Player;
	private readonly Label NameLabel;
	public readonly Label StatusLabel;

	public NameTagPanel(MafiaPlayer player)
	{
		this.Player = player;
		NameLabel = Add.Label( "Loading ..", "NameTag" );
		StatusLabel = Add.Label( string.Empty, "StatusLabel" );
		StyleSheet.Load( "/Hud/World/Player/NameTagPanel.scss" );
	}

	public override void Tick()
	{
		if ( !Player.IsValid() || Player is null || !Player.IsAlive || Player.Transmit != TransmitType.Always)
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

		StatusLabel.SetClass( "hide", StatusLabel.Text == string.Empty );
		base.Tick();
	}
}


