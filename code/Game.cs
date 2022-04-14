
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TerryTrials.Player;
using TerryTrials.State;

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
	partial class Game : Sandbox.Game
	{
		public static Game Instance
		{
			get => Current as Game;
		}

		public Game()
		{
			if (IsServer)
			{
				Global.TickRate = 30;
			}
			GameState = new LobbyState();
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

		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason )
		{
			base.ClientDisconnect( cl, reason );
		}

		private void OnStateChange(BaseState lastState, BaseState newState)
		{
			lastState?.Finish();
			newState?.Start();
		}
	}

}
