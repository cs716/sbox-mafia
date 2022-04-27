using TerryTrials.Player;
using TerryTrials.State;

namespace TerryTrials.Teams;
public partial class DoctorTeam : BaseTeam
{
	public override Alliance Alliance => Alliance.Town;
	public override string SingularName => "Doctor";
	public override string PluralName => "the Doctors";
	public override Color Color => Color.FromBytes( 255, 143, 143 );
	public override string Description => "You're the town's guardian angel. Select a town member each night to protect from the grasps of evil.";

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
