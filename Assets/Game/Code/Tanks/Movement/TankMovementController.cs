using Game.Code.Data.Configs;
using Game.Code.Tanks.Models;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Movement
{
	public class TankMovementController : IFixedTickable
	{
		[Inject] private TankUnitView _tankView;
		[Inject] private INetTankUnit _netTankUnit;
		[Inject] private Rigidbody _rigidbody;
		[Inject] private TankMovementModel _movementModel;
		[Inject] private TankConfig _tankConfig;

		
		public void FixedTick()
		{
			Transform transform = _tankView.transform;
			
			_netTankUnit.ServerSetVelocity(_rigidbody.velocity);
			
			Move(transform);
			Rotate(transform);
		}

		private void Move(Transform transform)
		{
			if (!IsMovementAllowed())
				return;
			
			Vector3 force = transform.forward * _movementModel.MoveInputValue * _tankConfig.EngineForce;

			_rigidbody.AddForce(force);
		}

		private void Rotate(Transform transform)
		{
			Quaternion rotation = transform.rotation;
			
			float angleDelta = _movementModel.RotateInputValue * _tankConfig.RotationSpeed * Time.deltaTime;
			Quaternion rotationDelta = Quaternion.Euler(0, angleDelta, 0);
			
			_rigidbody.MoveRotation(rotation * rotationDelta);
		}

		bool IsMovementAllowed()
		{
			var maxSpeed = _tankConfig.MaxForwardSpeed;
			
			if (_movementModel.Velocity.magnitude > maxSpeed)
				return false;

			// TODO: Also check ground and rotation
			
			return true;
		}
	}
}