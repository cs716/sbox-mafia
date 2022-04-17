using Sandbox;
using TerryTrials.State;

namespace TerryTrials;
partial class Game
{
	[Net, Change( nameof( OnStateChange ) )] public BaseState GameState { get; private set; }

	public void ChangeState( BaseState newState )
	{
		Assert.NotNull( newState );
		GameState?.Finish();
		GameState = newState;
		GameState?.Start();
	}
}
