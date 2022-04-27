using Sandbox;
using Sandbox.UI;
using System;

namespace TerryTrials.Hud.Overlay
{
	public partial class BlackOverlay : Panel
	{
		enum Fade
		{
			IN,
			OUT,
			NONE
		}

		private Fade FadeType { get; set; } = Fade.NONE;
		private decimal FadeAmount { get; set; }
		private decimal FadeEachTick { get; set; }

		public BlackOverlay()
		{
			Style.Opacity = 0;
			StyleSheet.Load( "/Hud/Overlay/BlackOverlay.scss" );
		}
		
		public void FadeIn( int time )
		{
			Style.Opacity = 1;
			FadeType = Fade.IN;
			FadeAmount = Global.TickRate * (decimal)time;
			FadeEachTick = 1 / FadeAmount;
		}
		public void FadeOut( int time )
		{
			Style.Opacity = 0;
			FadeType = Fade.OUT;
			FadeAmount = Global.TickRate * (decimal)time;
			FadeEachTick = 1 / FadeAmount;
		}

		public override void Tick()
		{
			if (FadeType == Fade.OUT)
			{
				Style.Opacity += ((float)FadeEachTick).Clamp(0, 1);
				if ( Style.Opacity >= 1 )
					FadeType = Fade.NONE;
			} else if (FadeType == Fade.IN)
			{
				Style.Opacity -= ((float)FadeEachTick).Clamp( 0, 1 );
				if ( Style.Opacity <= 0 )
				{
					FadeType = Fade.NONE;
				}
			}
			SetClass( "hide", Style.Opacity <= 0 );
			base.Tick();
		}
	}
}
