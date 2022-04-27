using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerryTrials.Player;
using TerryTrials.Teams;

namespace TerryTrials.State
{
	public partial class PreparingState : BaseState
	{
		public override void OnStart()
		{			
			if (Host.IsServer)
			{
				AssignRoles();
				Game.OverlayFadeOut( To.Everyone, 5 );
			}
			base.OnStart();
		}

		public override async Task OnFinish()
		{
			Game.OverlayFadeIn( To.Everyone, 5 );
			await base.OnFinish();
		}

		public static async void AssignRoles()
		{
			await Task.Delay( 3000 );
			var activePlayers = Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive ).ToList();

			var mafiaCount = Math.Floor( (activePlayers.Count * Game.MafiaPlayerRatio).Clamp(1, (activePlayers.Count / 2) + 1) );
			var detectiveCount = Math.Floor( (activePlayers.Count * Game.DetectivePlayerRatio).Clamp( 1, (activePlayers.Count / 2) + 1 ) );
			var doctorCount = Math.Floor( (activePlayers.Count * Game.DoctorPlayerRatio).Clamp( 1, (activePlayers.Count / 2) + 1 ) );

			for (int i = 0; i < mafiaCount; i++)
			{
				var random = new Random();
				var index = random.Next( activePlayers.Count );
				var player = activePlayers[index]; 

				player.Team = new MafiaTeam();
				Log.Info( $"{player.Client.Name} was assigned to Mafia" );
				activePlayers.RemoveAt( index );
			}

			for ( int i = 0; i < detectiveCount; i++ )
			{
				var random = new Random();
				var index = random.Next( activePlayers.Count );
				var player = activePlayers[index];

				player.Team = new DetectiveTeam();
				Log.Info( $"{player.Client.Name} was assigned to Detective" );
				activePlayers.RemoveAt( index );
			}

			for ( int i = 0; i < doctorCount; i++ )
			{
				var random = new Random();
				var index = random.Next( activePlayers.Count );
				var player = activePlayers[index];

				player.Team = new DoctorTeam();
				Log.Info( $"{player.Client.Name} was assigned to Doctor" );
				activePlayers.RemoveAt( index );
			}

			// Assign remainder to Town
			activePlayers.ForEach( player =>
			{
				player.Team = new TownTeam();
				Log.Info( $"{player.Client.Name} was assigned to Town" );
			} );

			Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive && p.Team is MafiaTeam ).ToList().ForEach( player =>
			{
				To list = To.Multiple( Entity.All.OfType<Client>().Where( c => (c.Pawn as MafiaPlayer).Team is MafiaTeam ).ToList() );
				LocalKnowns.RevealPlayerTeam( list, player.NetworkIdent, KnownType.ALLY, player.Team.SingularName );
			} );

			Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive && p.Team is DetectiveTeam ).ToList().ForEach( player =>
			{
				To list = To.Multiple( Entity.All.OfType<Client>().Where( c => (c.Pawn as MafiaPlayer).Team is DetectiveTeam ).ToList() );
				LocalKnowns.RevealPlayerTeam( list, player.NetworkIdent, KnownType.ALLY, player.Team.SingularName );
			} );

			Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive ).ToList().ForEach( player =>
			{
				var team = player.Team;
				Game.DisplayPlayerRoleToPlayer( To.Single( player ), team.UseFakeIdentity ? team.FakeSingularName : team.SingularName, team.Color, team.Description, true );
			} );

			await Task.Delay( 3000 );

			Game.HidePlayerRoleMenu( To.Everyone );

			Game.Instance.ChangeState( new NightState() );
		}
	}
}
