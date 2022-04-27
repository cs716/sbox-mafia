using Sandbox;
using TerryTrials.Hud;
using TerryTrials.Hud.World.Player;
using TerryTrials.Player;
using TerryTrials.State;

using System.Linq;
using TerryTrials.Teams;

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
	public HudController HudController;
	public LocalKnowns LocalKnowns; // Client only - Controls "Known" players

	public TownTeam TownTeam = new();
	public MafiaTeam MafiaTeam = new(); 
	public BaseTeam[] AddonTeams;
	
	[ServerVar( "tt_mafia_player_ratio", Help = "Ratio of Town to Mafia players", Min = 0.2f, Max = 0.4f )]
	public static float MafiaPlayerRatio { get; set; } = 0.25f;
	[ServerVar( "tt_doctor_player_ratio", Help = "Ratio of Town to Doctor players", Min = 0.2f, Max = 0.4f )]
	public static float DoctorPlayerRatio { get; set; } = 0.2f;
	[ServerVar( "tt_detective_player_ratio", Help = "Ratio of Town to Detective players", Min = 0.2f, Max = 0.4f )]
	public static float DetectivePlayerRatio { get; set; } = 0.2f;

	public Game()
	{
		if ( IsServer )
		{
			Global.TickRate = 30;
			HudController = new HudController();
			//InitTeams();
		} else if (IsClient)
		{
			LocalKnowns = new();
		}
		ChangeState( new LobbyState() );
		NameTagHandler = new();
	}

	/*public void InitTeams()
	{
		// FUTURE: Any teams besides Mafia and Town will go here
	}*/

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

	public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
	{
		GameState?.OnPlayerLeave( cl );
		base.ClientDisconnect( cl, reason );
	}

	private void OnStateChange( BaseState lastState, BaseState newState )
	{
		lastState?.Finish();
		newState?.Start();
	}

	[ServerCmd(name: "overlay_test")]
	public static void TestOverlay()
	{
		var player = ConsoleSystem.Caller.Pawn as MafiaPlayer;
		var team = new MafiaTeam();
		DisplayPlayerRoleToPlayer( To.Single( player ), team.UseFakeIdentity == true ? team.FakeSingularName : team.SingularName, team.Color, team.Description, true );
	}
}
