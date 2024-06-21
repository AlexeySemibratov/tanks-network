using Game.Code.Tanks.Models;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks
{
	public class NetTankUnit : NetworkBehaviour, INetTankUnit
	{
		[Inject] private TankMovementModel _movementModel;
		
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
	}
}