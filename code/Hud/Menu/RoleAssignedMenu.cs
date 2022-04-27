using Sandbox.UI;

namespace TerryTrials.Hud.Menu
{
	[UseTemplate]
	public partial class RoleAssignedMenu : Panel
	{
		public Label Role { get; set; }
		public string RoleDescription { get; set; }
		public bool Show = false;

		public override void Tick()
		{
			SetClass( "hide", !Show );
			base.Tick();
		}
	}
}
