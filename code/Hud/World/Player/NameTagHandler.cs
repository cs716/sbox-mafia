using Sandbox;
using System.Collections.Generic;
using System.Linq;
using TerryTrials.Player;

namespace TerryTrials.Hud.World.Player;

public partial class NameTagHandler
{
	public Dictionary<Entity, NameTagPanel> NameTags { get; set; } = new();

	public NameTagHandler()
	{
		Event.Register( this );
	}

	[Event.Tick]
	public void OnTick()
	{
		if (Host.IsClient)
		{
			foreach ( MafiaPlayer player in Entity.All.OfType<MafiaPlayer>().Where( p => p.IsAlive && p.Transmit == TransmitType.Always ) )
			{
				if ( !NameTags.ContainsKey( player ) )
				{
					var nametag = new NameTagPanel( player );
					NameTags.Add( player, nametag );
				}
			}
		}
	}
}

