using Sandbox;
using System.Linq;
using TerryTrials.Player;

namespace TerryTrials.State;
public partial class LobbyState : BaseState
{

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
				break;
			}
		}
	}
}
