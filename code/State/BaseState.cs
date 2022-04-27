using Sandbox;
using System.Threading.Tasks;
using TerryTrials.Hud;
using TerryTrials.Player;

namespace TerryTrials.State;
public abstract partial class BaseState : BaseNetworkable
{

	public void Start()
	{
		OnStart();
	}

	public async void Finish()
	{
		await OnFinish();
	}

	public virtual void OnStart() { }
	public virtual async Task OnFinish() {
		if ( Host.IsClient )
		{
			// Clear all status panels on state change
			var tagPanels = Local.Hud.ChildrenOfType<NameTagPanel>();
			foreach ( var tagPanel in tagPanels )
			{
				tagPanel.StatusLabel.Text = string.Empty;
			}
		}

		await Task.Delay( 0 ); 
	} // Suppress the warning if unused

	public virtual void OnPlayerLeave(Client cl) { }
	public virtual void OnPlayerJoin( MafiaPlayer player ) { }
	public virtual void OnTick() { }
	public virtual void OnSecond() { }
}
