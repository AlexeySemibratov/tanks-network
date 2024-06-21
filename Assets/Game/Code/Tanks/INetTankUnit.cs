using UnityEngine;

namespace Game.Code.Tanks
{
	public interface INetTankUnit
	{
		void ServerSetVelocity(Vector3 velocity);
		
		void CmdShoot();
		void CmdMoveAxisInput(float value);
		void CmdRotateAxisInput(float value);
	}
}