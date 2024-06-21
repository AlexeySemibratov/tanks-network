using UnityEngine;

namespace Game.Code.Tanks
{
	public interface INetTankUnit
	{
		void CmdShoot();

		void CmdSetVerticalInput(Vector2 dir);
	}
}