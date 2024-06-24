using Game.Code.Tanks;
using Mirror;
using UnityEngine;

namespace Game.Code.Network.Spawners
{
	public interface INetTankSpawner
	{
		NetTankUnit ServerSpawn(NetworkConnectionToClient conn, Transform point);

		void ServerUnpsawn(NetworkConnectionToClient conn);
	}
}