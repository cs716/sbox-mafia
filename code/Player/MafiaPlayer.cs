using Sandbox;
using Sandbox.UI;
using TerryTrials.Hud;
using TerryTrials.Teams;

namespace TerryTrials.Player;
public partial class MafiaPlayer : Sandbox.Player
{
	[Net] public bool IsAlive { get; private set; } = false;
	[Net] public bool IsReady { get; set; } = false;

	public int HomePlateIdent { get; set; }
	public bool IsProtected { get; set; } = false;
	public BaseTeam Team { get; set; }

	public Clothing.Container Clothing = new();

	public MafiaPlayer()
	{
	}

	public MafiaPlayer( Client client ) : this()
	{
		Clothing.LoadFromClient( client );
	}

	public override void OnKilled()
	{
		IsAlive = false;
		base.OnKilled();
	}

	public override void Respawn()
	{
		CameraMode = new PlayerCamera();
		Animator = new StandardPlayerAnimator();

		EnableAllCollisions = true;
		EnableDrawing = true;

		SetModel( "models/citizen/citizen.vmdl" );

		foreach ( var child in Children )
			child.EnableDrawing = true;

		Clothing.DressEntity( this );

		IsAlive = true;

		base.Respawn();
	}
}
