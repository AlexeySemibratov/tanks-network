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
		[Inject] private TankInputModel _inputModel;
		[Inject] private TankConfig _tankConfig;

		
		public void FixedTick()
		{
			UpdateModel();
			Move();
			Rotate();
		}

		private void UpdateModel()
		{
			_netTankUnit.ServerSetVelocity(_rigidbody.velocity);
			
			if (_inputModel.MoveInputValue < 0 && _tankView.transform.InverseTransformDirection(_rigidbody.velocity).z < 1)
				_movementModel.MoveDirection = -1;
			else
				_movementModel.MoveDirection = 1;
		}

		private void Move()
		{
			MoveWheels(_tankView.WheelsL, true);
			MoveWheels(_tankView.WheelsR, false);
		}

		private void MoveWheels(TankWheel[] wheels, bool isLeftWheels)
		{
			float dirSign = isLeftWheels ? 1f : -1f;
			float brakeTorque = _tankConfig.EngineBreakTorque * GetBrakeDirValue();
			
			foreach (var wheel in wheels)
			{
				float dir = Mathf.Clamp(_inputModel.MoveInputValue + dirSign * _inputModel.RotateInputValue, -1f, 1f);
				float torque = dir * _tankConfig.EngineGasTorque;
				
				if (CanMoveWheel(wheel))
					wheel.SetMotorTorque(torque);

				wheel.SetBrakeTorque(brakeTorque);
			}
		}

		private float GetBrakeDirValue() => _inputModel.BrakePressed ? 1 : 0;

		private void Rotate()
		{
			if(_tankView.MiddleWheelL.IsGrounded || _tankView.MiddleWheelR.IsGrounded)
			{
				Vector3 torque = Vector3.up * _inputModel.RotateInputValue * _tankConfig.EngineRotationTorque;
				
				_rigidbody.AddRelativeTorque(torque);
			}
		}

		bool CanMoveWheel(TankWheel wheel)
		{
			var maxSpeed = _tankConfig.MaxForwardSpeed;

			return _movementModel.Velocity.magnitude < maxSpeed && wheel.IsGrounded;
		}
	}
}