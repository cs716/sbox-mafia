
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TerryTrials.Player;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace TerryTrials
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class TerryTrials : Sandbox.Game
	{
		public TerryTrials()
		{

		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			// Create a pawn for this client to play with
			var player = new MafiaPlayer( client );
			client.Pawn = player;

			// Get all of the spawnpoints
			var spawnpoints = Entity.All.OfType<SpawnPoint>();

			foreach( var spawnPoint in spawnpoints )
			{
				bool available = !All.OfType<MafiaPlayer>().Where( player => player.SpawnPointId == spawnPoint.NetworkIdent ).Any();
				if (available)
				{
					player.SpawnPointId = spawnPoint.NetworkIdent;
					
					player.Position = spawnPoint.Position;
					player.Rotation = spawnPoint.Rotation;

					player.Respawn();
				}
			}
		}

		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
		{
			base.ClientDisconnect( cl, reason );
		}
	}

}
