using Sandbox;

namespace TerryTrials.State;
public partial class DayState : BaseState
{
	[ServerVar( "tt_day_phase_length", Help = "Time (in seconds) for the day phase", Min = 30, Max = 600 )]
	public static float DayStateLengthSeconds { get; set; } = 300;
	public RealTimeUntil NightStateChange { get; private set; }

	public override void OnStart()
	{
		NightStateChange = DayStateLengthSeconds;
		base.OnStart();
	}

	public override void OnTick()
	{
		if ( NightStateChange <= 0 )
		{

		}
		base.OnTick();
	}
}
