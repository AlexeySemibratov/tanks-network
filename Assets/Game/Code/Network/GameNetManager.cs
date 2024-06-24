using Game.Code.Network.Spawners;
using Game.Code.Tanks;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Network
{
	public class GameNetManager : NetworkManager
	{
		[SerializeField] private Transform _spawnPoint;
		
		[Inject] private INetTankSpawner _netTankSpawner;
		[Inject] private INetModeProvider _netModeProvider;
		
		public override void Start()
		{
			base.Start();

			switch (_netModeProvider.NetMode)
			{
				case ENetMode.Client:
					StartClient();
					break;
				case ENetMode.Server:
					StartServer();
					break;
				case ENetMode.Host:
					StartHost();
					break;
			}
		}

		public override void OnServerConnect(NetworkConnectionToClient conn)
		{
			NetTankUnit tank = _netTankSpawner.ServerSpawn(conn, _spawnPoint);
			
			NetworkServer.AddPlayerForConnection(conn, tank.gameObject);
		}

		public override void OnServerDisconnect(NetworkConnectionToClient conn)
		{
			_netTankSpawner.ServerUnpsawn(conn);
		}
	}
}