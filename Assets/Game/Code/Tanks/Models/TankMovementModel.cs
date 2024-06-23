using UnityEngine;

namespace Game.Code.Tanks.Models
{
	public class TankMovementModel
	{
		public Vector3 Velocity { get; set; } = Vector3.zero;
		public int MoveDirection { get; set; }
	}
}