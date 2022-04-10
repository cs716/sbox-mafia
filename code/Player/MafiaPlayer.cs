using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerryTrials.Player
{
	public partial class MafiaPlayer : Sandbox.Player
	{
		[Net] public int SpawnPointId { get; set; }

		public Clothing.Container Clothing = new();

		public MafiaPlayer()
		{

		}

		public MafiaPlayer(Client client) : this()
		{
			Clothing.LoadFromClient( client );
		}

		public override void Respawn()
		{
			CameraMode = new ThirdPersonCamera();
			Animator = new StandardPlayerAnimator();

			EnableAllCollisions = true;
			EnableDrawing = true;

			SetModel( "models/citizen/citizen.vmdl" );

			foreach ( var child in Children )
				child.EnableDrawing = true;

			Clothing.DressEntity( this );

			base.Respawn();
		}
	}
}
