using Sandbox;
using TerryTrials.Hud;
using TerryTrials.Hud.World.Player;
using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials;
/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players.
/// </summary>
partial class Game : Sandbox.Game
{
	public static Game Instance
	{
		get => Current as Game;
	}

	public NameTagHandler NameTagHandler;

	public Game()
	{
		if ( IsServer )
		{
			Global.TickRate = 30;
			_ = new HudController();
		}
		ChangeState( new IdleState() );
		NameTagHandler = new();
	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		var player = new MafiaPlayer( client );
		client.Pawn = player;

		GameState?.OnPlayerJoin( player );
	}

	public override void PostLevelLoaded()
	{
		ChangeState( new LobbyState() );
		base.PostLevelLoaded();
	}

	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{
		base.ClientDisconnect( cl, reason );
	}

	private void OnStateChange( BaseState lastState, BaseState newState )
	{
		lastState?.Finish();
		newState?.Start();
	}
}
