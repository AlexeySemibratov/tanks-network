using Game.Code.Tanks.Models;
using Game.Code.Tanks.Movement;
using Mirror;
using UniRx;
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
		
		[SyncVar(hook = nameof(OnMoveDirChanged))] 
		private EMoveDirection _moveDirection;

		public void ServerSetVelocity(Vector3 velocity)
		{
			MovementModel.Velocity.Value = velocity;
			MovementModel.VelocityMagnitude.Value = velocity.magnitude;
			_velocity = velocity;
		}

		public void ServerSetMoveDirection(EMoveDirection dir)
		{
			MovementModel.MoveDirection = dir;
			_moveDirection = dir;
		}

		private void OnVelocityChanged(Vector3 _, Vector3 value)
		{
			MovementModel.Velocity.Value = value;
			MovementModel.VelocityMagnitude.Value = value.magnitude;
		}

		private void OnMoveDirChanged(EMoveDirection _, EMoveDirection value)
		{
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