using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.State
{
	public partial class LobbyState : BaseState
	{
		public override void OnPlayerJoin(MafiaPlayer player)
		{
			var spawnpoints = Entity.All.OfType<SpawnPoint>();

			foreach ( var spawnPoint in spawnpoints )
			{
				bool available = !Entity.All.OfType<MafiaPlayer>().Where( player => player.SpawnPointId == spawnPoint.NetworkIdent ).Any();
				if ( available )
				{
					player.SpawnPointId = spawnPoint.NetworkIdent;

					player.Position = spawnPoint.Position;
					player.Rotation = spawnPoint.Rotation;

					player.Respawn();
				}
			}
		}
	}
}
