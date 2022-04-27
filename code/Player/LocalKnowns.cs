using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerryTrials.Teams;

namespace TerryTrials.Player
{
	public enum KnownType
	{
		ALLY,
		ENEMY,
		NEUTRAL,
		UNKNOWN = -1
	}
	public struct LocalKnown
	{
		public string TeamName { get; set; } = "";
		public KnownType KnownType { get; set; } = KnownType.UNKNOWN;
		public Alliance Alliance { get; set; } = Alliance.ThirdParty;
	}
	public partial class LocalKnowns
	{
		public Dictionary<MafiaPlayer, LocalKnown> KnownList { get; set; } = new();
		private readonly LocalKnown UnknownPlayer = new();

		public static LocalKnowns Instance
		{
			get => Game.Instance.LocalKnowns;
		}

		public LocalKnown GetKnown(MafiaPlayer player)
		{
			if ( KnownList.ContainsKey( player ) )
				return KnownList[player];
			else
				return UnknownPlayer;
		}

		[ClientRpc] public static void RevealPlayerTeam( int netId, KnownType knownType, string teamName )
		{
			MafiaPlayer player = Entity.FindByIndex( netId ) as MafiaPlayer;
			Log.Info( "Received team of " + netId + " - " + teamName );
			if ( player is not null && player.IsValid() && player.IsAlive )
			{
				Instance.KnownList[player] = new LocalKnown() { TeamName=teamName, KnownType=knownType };
			}
		}
	}
}
