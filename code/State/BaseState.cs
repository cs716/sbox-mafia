using Sandbox;

using TerryTrials.Player;

namespace TerryTrials.State
{
	public abstract partial class BaseState : BaseNetworkable
	{
		[Net] public virtual RealTimeUntil StateTimer { get; set; } = 0f;

		public void Start()
		{
			OnStart();
		}

		public void Finish()
		{
			OnFinish();
		}

		protected virtual void OnStart() { }
		protected virtual void OnFinish() { }

		public virtual void OnPlayerLeave() { }
		public virtual void OnPlayerJoin(MafiaPlayer player) { }
		public virtual void OnTick() { }
		public virtual void OnSecond() { }

	}
}
