using Game.Code.Tanks.Models;
using Game.Code.Tanks.Movement;
using Game.Code.Extensions;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks
{
	public class NetTankUnit : NetworkBehaviour, INetTankUnit
	{
		[field:SerializeField] public NetworkIdentity AssetNetIdentity { get; private set; }

		public int Id => _id;

		[Inject] public TankMovementModel MovementModel { get; private set; }

		[Inject] private TankInputModel _inputModel;
		
		// Server only
		[InjectOptional] private TankResetController _resetController;

		#region Initialization

		[SyncVar] 
		private int _id;
		
		public void ServerInit(int id)
		{
			_id = id;
		}

		#endregion
		
		
		[SyncVar(hook = nameof(OnVelocityChanged))] 
		private Vector3 _velocity;
		
		[SyncVar(hook = nameof(OnAngularVelocityChanged))] 
		private Vector3 _angularVelocity;
		
		[SyncVar(hook = nameof(OnMoveDirChanged))] 
		private EMoveDirection _moveDirection;

		public void ServerSetVelocity(Vector3 velocity)
		{
			OnVelocityChanged(default, velocity);
			_velocity = velocity;
		}

		public void ServerSetAngularVelocity(Vector3 velocity)
		{
			OnAngularVelocityChanged(default, velocity);
			_angularVelocity = velocity;
		}

		public void ServerSetMoveDirection(EMoveDirection dir)
		{
			OnMoveDirChanged(default, dir);
			_moveDirection = dir;
		}

		private void OnVelocityChanged(Vector3 _, Vector3 value)
		{
			MovementModel?.Velocity.Set(value);
			MovementModel?.VelocityMagnitude.Set(value.magnitude);
		}
		
		private void OnAngularVelocityChanged(Vector3 _, Vector3 value)
		{
			MovementModel?.AngularVelocity.Set(value);
		}

		private void OnMoveDirChanged(EMoveDirection _, EMoveDirection value)
		{
			if (MovementModel != null)
				MovementModel.MoveDirection = value;
		}

		#region Commands
		
		[Command]
		public void CmdSetBrakeInput(bool pressed)
		{
			_inputModel.BrakePressed = pressed;
		}

		[Command]
		public void CmdMoveAxisInput(float value)
		{
			_inputModel.MoveInputValue = value;
		}

		[Command]
		public void CmdRotateAxisInput(float value)
		{
			_inputModel.RotateInputValue = value;
		}

		[Command]
		public void CmdReset()
		{
			_resetController.Reset();
		}

		#endregion
	}
}