using UniRx;
using UnityEngine;

namespace Game.Code.Tanks.Models
{
	public class TankMovementModel
	{
		public readonly ReactiveProperty<Vector3> Velocity = new(Vector3.zero);
		public readonly ReactiveProperty<Vector3> AngularVelocity = new(Vector3.zero);
		public readonly FloatReactiveProperty VelocityMagnitude = new();
		
		public EMoveDirection MoveDirection { get; set; } = EMoveDirection.None;
	}
}