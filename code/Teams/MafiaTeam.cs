using TerryTrials.Player;

namespace TerryTrials.Teams;

public partial class MafiaTeam : BaseTeam
{
	public override Alliance Alliance => Alliance.Mafia;
	public override string SingularName => "Mafia";
	public override string PluralName => "the Mafia";
	public override Color Color => Color.FromBytes(171, 0, 0);
	public override string Description => "Work with the other Mafia members and your allies to eliminate those against you. ";
	public override void OnPlayerChat( MafiaPlayer player, string Message )
	{
		base.OnPlayerChat( player, Message );
	}
}

