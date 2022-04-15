using Sandbox;

using TerryTrials.Player;

namespace TerryTrials.State
{
	public abstract partial class BaseState : BaseNetworkable
	{

		public void Start()
		{
			OnStart();
		}

		public void Finish()
		{
			OnFinish();
		}

		public virtual void OnStart() { }
		public virtual void OnFinish() { }

		public virtual void OnPlayerLeave() { }
		public virtual void OnPlayerJoin(MafiaPlayer player) { }
		public virtual void OnTick() { }
		public virtual void OnSecond() { }

	}
}
