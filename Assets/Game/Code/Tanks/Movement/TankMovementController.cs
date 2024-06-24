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
			var velocity = _rigidbody.velocity;
			var angularVelocity = _rigidbody.angularVelocity;
			
			_netTankUnit.ServerSetVelocity(velocity);
			_netTankUnit.ServerSetAngularVelocity(angularVelocity);

			if (IsMovingBackward(velocity))
				_netTankUnit.ServerSetMoveDirection(EMoveDirection.Backward);
			else
				_netTankUnit.ServerSetMoveDirection(EMoveDirection.Forward);
		}

		private bool IsMovingBackward(Vector3 rigidbodyVelocity)
			=>
				_inputModel.MoveInputValue < 0 &&
				_tankView.transform.InverseTransformDirection(rigidbodyVelocity).z < 1;

		private void Move()
		{
			MoveWheels(_tankView.WheelsL, true);
			MoveWheels(_tankView.WheelsR, false);
		}

		private void MoveWheels(TankWheel[] wheels, bool isLeftWheels)
		{
			float dirSign = isLeftWheels ? 1f : -1f;
			float brakeTorque = _tankConfig.EngineBreakTorque * _inputModel.BrakeInputValue;
			bool isSpeedLimitReached = IsVelocityLimitReached();

			if (_movementModel.MoveDirection == EMoveDirection.Backward)
				dirSign *= -1;
			
			foreach (var wheel in wheels)
			{
				if (!isSpeedLimitReached && wheel.IsGrounded)
				{
					float torqueDir = Mathf.Clamp(_inputModel.MoveInputValue + dirSign * _inputModel.RotateInputValue, -1f, 1f);
					float torque = torqueDir * _tankConfig.EngineGasTorque;
					
					wheel.SetMotorTorque(torque);
				}
				else
				{
					wheel.SetMotorTorque(0);
				}

				wheel.SetBrakeTorque(brakeTorque);
			}
		}

		private bool IsVelocityLimitReached()
		{
			var currVelocity = _movementModel.VelocityMagnitude.Value;

			var maxVelocity = _movementModel.MoveDirection switch
			{
				EMoveDirection.Forward => _tankConfig.MaxForwardSpeed,
				EMoveDirection.Backward => _tankConfig.MaxBackwardSpeed,
				_ => Mathf.Infinity
			};
			
			return currVelocity > maxVelocity;
		}

		private void Rotate()
		{
			if (!IsRotationAllowed()) 
				return;
			
			Vector3 torque = Vector3.up * _inputModel.RotateInputValue * _tankConfig.EngineRelativeTorqueAcceleration;
				
			_rigidbody.AddRelativeTorque(torque, ForceMode.Acceleration);
		}

		private bool IsRotationAllowed()
		{
			var angularVelocityY = Mathf.Abs(_movementModel.AngularVelocity.Value.y);
			
			return _tankView.MiddleWheelL.IsGrounded &&
			       _tankView.MiddleWheelR.IsGrounded &&
			       angularVelocityY <= _tankConfig.MaxAngularSpeed;
		}
	}
}