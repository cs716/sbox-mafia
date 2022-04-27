using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.Teams;
public partial class DetectiveTeam : BaseTeam
{
	public override Alliance Alliance => Alliance.Town;
	public override string SingularName => "Detective";
	public override string PluralName => "the Detectives";
	public override Color Color => Color.FromBytes( 0, 115, 255 );
	public override string Description => "Work together with other Detectives to learn the roles and intentions of those around you.";
	public override void OnPlayerChat( MafiaPlayer player, string Message )
	{
		if ( Game.Instance.GameState is not NightState )
			base.OnPlayerChat( player, Message );
		else
		{
			//TODO: Chat message saying you can't talk during night actions
		}
	}
}
