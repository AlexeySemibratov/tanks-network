using Game.Code.Tanks.Models;
using UnityEngine;

namespace Game.Code.Tanks
{
	public interface INetTankUnit
	{
		int Id { get; }

		TankMovementModel MovementModel { get; }

		void ServerInit(int id);
		void ServerSetVelocity(Vector3 velocity);
		void ServerSetMoveDirection(EMoveDirection dir);
		
		void CmdSetBrakeInput(bool pressed);
		void CmdMoveAxisInput(float value);
		void CmdRotateAxisInput(float value);
		void CmdReset();
	}
}