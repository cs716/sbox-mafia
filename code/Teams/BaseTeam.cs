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

	public virtual bool IsUsed { get; set; } = true; // Enabled when teams are assigned if it is used. Defaults to true, can be set to false if unused.
	public virtual Alliance Alliance => Alliance.ThirdParty; // Town, Mafia or ThirdParty. Controls point assignments on a win condition
	public virtual string SingularName => "Unknown"; // Friendly name for the role in singular form
	public virtual string PluralName => "Unknown"; // Friendly name for the role in plural form
	public virtual string FakeSingularName => "Unknown";
	public virtual string FakePluralName => "Unknowns";
	public virtual string Description => "Unknown";
	public virtual Color Color => Color.White;
	public virtual bool UseFakeIdentity => false;
	public virtual BaseAction NightAction => null; // Night action for this role (if any)
	public virtual BaseAction DayAction => null; // Day action for this role (if any)
	public virtual int MinimumPlayers => 0; // Minimum players that must be playing for this role to become active in the rotation
	public virtual double GetAssignRatio()
	{
		return 0.0;
	}
	public virtual void OnPlayerChat( MafiaPlayer player, string Message ) { }
}
