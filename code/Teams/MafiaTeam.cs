using TerryTrials.Player;

namespace TerryTrials.Teams;

public partial class MafiaTeam : BaseTeam
{
	[ServerVar("tt_mafia_player_ratio", Help = "Ratio of Town to Mafia players", Min = 0.4f, Max = 0.1f)]
	public static float MafiaPlayerRatio { get; set; } = 0.25f;
	public override Alliance Alliance => Alliance.Mafia;
	public override string SingularName => "Mafia";
	public override string PluralName => "the Mafia";
	public override int MinimumPlayers => 0;
	public override double GetAssignRatio()
	{
		return MafiaPlayerRatio;
	}

	public override void OnPlayerChat( MafiaPlayer player, string Message )
	{
		base.OnPlayerChat( player, Message );
	}
}

