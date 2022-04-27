using Sandbox;
using System;
using System.Linq;
using TerryTrials.Player;

namespace TerryTrials.State;
public partial class LobbyState : BaseState
{
	[Net] public bool LobbyCountdownStarted { get; set; } = false;
	[Net] public bool ReadyStateLocked { get; set; } = false;
	[Net] public int LobbyCountdown { get; set; } = 0;

	public override void OnStart()
	{
		base.OnStart();
		// Find any existing players that joined during Idle and initialize them

		var players = Entity.All.OfType<MafiaPlayer>();
		if ( players.Any() )
		{
			foreach ( var player in players.ToList() )
			{
				OnPlayerJoin( player );
			}
		}
	}

	public override void OnSecond()
	{
		decimal readyPlayers = Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive && p.IsReady ).Count();
		decimal totalPlayers = Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive ).Count();
		decimal ratio = readyPlayers / totalPlayers * 100;
		if (totalPlayers >= 5 && ratio >= 50)
		{
			if ( LobbyCountdownStarted ) { 
				LobbyCountdown--;
				if (LobbyCountdown <= 0)
				{
					Game.Instance.ChangeState( new PreparingState() );
				}
			}
			else
			{
				LobbyCountdown = 5;
				LobbyCountdownStarted = true;
			} 
		} else if(LobbyCountdownStarted)
			LobbyCountdownStarted = false;

		base.OnSecond();
	}
	public override void OnPlayerJoin( MafiaPlayer player )
	{
		var spawnpoints = Entity.All.OfType<SpawnPoint>();

		foreach ( var spawnPoint in spawnpoints.ToList() )
		{
			Vector3 potentialSpawn = spawnPoint.Position;
			bool occupied = Entity.FindInSphere( potentialSpawn, 5 ).OfType<MafiaPlayer>().Where( p => p.IsAlive ).Any();
			if ( !occupied )
			{
				player.Respawn();
				player.Position = potentialSpawn;
				player.Rotation = spawnPoint.Rotation;
				player.HomePlateIdent = spawnPoint.NetworkIdent;

				if ( player.Client.IsBot )
					player.IsReady = true;

				break;
			}
		}
	}
}
