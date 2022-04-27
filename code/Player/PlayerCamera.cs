using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerryTrials.Player
{
	public partial class PlayerCamera : CameraMode
	{
		public float CameraDistance = 170f;

		float CameraDistanceMax = 220f;
		float CameraDistanceMin = 100f;

		public override void Update()
		{
			if ( Local.Pawn is not AnimEntity pawn )
				return;

			Position = pawn.Position;
			Vector3 targetPos;

			var center = pawn.Position + Vector3.Up * 64;

			Position = center;
			Rotation = Rotation.FromAxis( Vector3.Up, 4 ) * Input.Rotation;

			float distance = CameraDistance * pawn.Scale;
			targetPos = Position + Input.Rotation.Right * ((pawn.CollisionBounds.Maxs.x + 15) * pawn.Scale);
			targetPos += Input.Rotation.Forward * -distance;

			Position = targetPos;
			FieldOfView = 70;

			Viewer = null;
		}

		public void ZoomIn()
		{
			CameraDistance = Math.Clamp( CameraDistance + 10, CameraDistanceMin, CameraDistanceMax );
		}

		public void ZoomOut()
		{
			CameraDistance = Math.Clamp( CameraDistance - 10, CameraDistanceMin, CameraDistanceMax );
		}
	}
}
