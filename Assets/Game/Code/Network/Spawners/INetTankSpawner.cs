using Game.Code.Tanks;
using Mirror;

namespace Game.Code.Network.Spawners
{
	public interface INetTankSpawner
	{
		NetTankUnit ServerSpawn(NetworkConnectionToClient conn);

		void ServerUnpsawn(NetworkConnectionToClient conn);
	}
}