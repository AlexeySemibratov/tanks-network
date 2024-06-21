using Game.Code.Tanks.Models;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Movement
{
	public class TankMovement : IFixedTickable
	{
		[Inject] private TankUnitView _tankView;
		[Inject] private Rigidbody _rigidbody;
		[Inject] private TankMovementModel _movementModel;

		// TODO: Move to config
		private const float MoveSpeed = 10.5f;
		private const float RotationSpeed = 150f;

		
		public void FixedTick()
		{
			Transform transform = _tankView.transform;
			
			Debug.Log($"Velocity {_rigidbody.velocity}");
			
			Move(transform);
			Rotate(transform);
		}

		private void Move(Transform transform)
		{
			Vector3 position = transform.position;
			Vector3 moveDelta = transform.forward * _movementModel.MoveInputValue * MoveSpeed * Time.deltaTime;

			_rigidbody.MovePosition(position + moveDelta);
		}

		private void Rotate(Transform transform)
		{
			Quaternion rotation = transform.rotation;
			
			float angleDelta = _movementModel.RotateInputValue * RotationSpeed * Time.deltaTime;
			Quaternion rotationDelta = Quaternion.Euler(0, angleDelta, 0);
			
			_rigidbody.MoveRotation(rotation * rotationDelta);
		}
	}
}