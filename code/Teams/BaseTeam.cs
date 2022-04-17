using TerryTrials.Actions;
using TerryTrials.Player;

namespace TerryTrials.Teams;
public enum Alliance
{
	Town,
	Mafia,
	ThirdParty = -1
}

public abstract partial class BaseTeam
{
	public virtual Alliance Alliance => Alliance.ThirdParty; // Town, Mafia or ThirdParty. Controls point assignments on a win condition
	public virtual string SingularName => "Unknown"; // Friendly name for the role in singular form
	public virtual string PluralName => "Unknown"; // Friendly name for the role in plural form
	public virtual BaseAction NightAction => null; // Night action for this role (if any)
	public virtual BaseAction DayAction => null; // Day action for this role (if any)
	public virtual int MinimumPlayers => 0; // Minimum players that must be playing for this role to become active in the rotation
	public virtual double GetAssignRatio()
	{
		return 0.0;
	}
	public virtual void OnPlayerChat( MafiaPlayer player, string Message ) { }
}
