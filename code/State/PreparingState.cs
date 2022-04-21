using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerryTrials.State
{
	public partial class PreparingState : BaseState
	{
		public override void OnStart()
		{
			Log.Info( "PreparingState Started" );
			base.OnStart();
		}
	}
}
