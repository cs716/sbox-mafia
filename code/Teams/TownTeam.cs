using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.Teams;

public partial class TownTeam : BaseTeam
{ 
	public override Alliance Alliance => Alliance.Town;
	public override string SingularName => "Villager";
	public override string PluralName => "the Villagers";
	public override Color Color => Color.FromBytes( 133, 133, 133 );
	public override string Description => "Survive and work with your fellow town members to identify and eliminate those that wish you harm.";
	public override void OnPlayerChat( MafiaPlayer player, string Message )
	{
		if (Game.Instance.GameState is not NightState)
			base.OnPlayerChat( player, Message );
		else
		{
			//TODO: Chat message saying you can't talk during night actions
		}
	}
}

