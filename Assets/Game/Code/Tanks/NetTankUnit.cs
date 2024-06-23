using Game.Code.Tanks.Models;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks
{
	public class NetTankUnit : NetworkBehaviour, INetTankUnit
	{
		[field:SerializeField] public NetworkIdentity AssetNetIdentity { get; private set; }

		public int Id => _id;

		[Inject] private TankMovementModel _movementModel;
		[Inject] private TankInputModel _inputModel;

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

		public void ServerSetVelocity(Vector3 velocity)
		{
			_movementModel.Velocity = velocity;
			_velocity = velocity;
		}

		private void OnVelocityChanged(Vector3 _, Vector3 value)
		{
			_movementModel.Velocity = value;
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
		
		#endregion
	}
}