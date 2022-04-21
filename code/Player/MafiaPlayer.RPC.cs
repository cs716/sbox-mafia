using Sandbox;

namespace TerryTrials.Player;
public partial class MafiaPlayer : Sandbox.Player
{
	[ServerCmd]
	public static void ToggleReadyState(bool ReadyState)
	{
		MafiaPlayer caller = ConsoleSystem.Caller.Pawn as MafiaPlayer;
		caller.IsReady = ReadyState;
	}
}
