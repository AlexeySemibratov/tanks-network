using Game.Code.Tanks.Models;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks
{
	public class NetTankUnit : NetworkBehaviour, INetTankUnit
	{
		[Inject] private TankMovementModel _movementModel;

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
		
		public void CmdShoot()
		{
			Debug.Log("Fire!");
		}

		[Command]
		public void CmdMoveAxisInput(float value)
		{
			_movementModel.MoveInputValue = value;
		}

		[Command]
		public void CmdRotateAxisInput(float value)
		{
			_movementModel.RotateInputValue = value;
		}
		
		#endregion
	}
}