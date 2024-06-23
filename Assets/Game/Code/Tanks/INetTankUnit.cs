using UnityEngine;

namespace Game.Code.Tanks
{
	public interface INetTankUnit
	{
		int Id { get; }

		void ServerInit(int id);
		void ServerSetVelocity(Vector3 velocity);
		
		void CmdShoot();
		void CmdMoveAxisInput(float value);
		void CmdRotateAxisInput(float value);
	}
}