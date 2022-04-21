using Sandbox;
using TerryTrials.State;

namespace TerryTrials;
partial class Game
{
	[Net, Change( nameof( OnStateChange ) )] public BaseState GameState { get; private set; }
	private TimeSince TimeSinceLastSecond { get; set; }

	public void ChangeState( BaseState newState )
	{
		Assert.NotNull( newState );
		GameState?.Finish();
		GameState = newState;
		GameState?.Start();
	}

	[Event.Tick]
	public void OnSecond()
	{
		if ( TimeSinceLastSecond < 1 )
			return;

		TimeSinceLastSecond = 0;

		if ( !Host.IsServer )
			return;

		GameState?.OnSecond();
	}
}
