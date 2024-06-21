using System.Collections.Generic;
using Game.Code.Data.Configs;
using Game.Code.Data.Providers;
using Game.Code.Tanks;
using Game.Code.Tanks.Factory;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Network.Spawners
{
	public class NetTankSpawner : INetTankSpawner, IInitializable
	{
		[Inject] private IGameDataProvider _gameDataProvider;
		[Inject] private INetModeProvider _netModeProvider;
		[Inject] private ITankFactory _tankFactory;

		private GameConfig GameConfig => _gameDataProvider.GameConfig;
		private NetTankUnit TankUnitPrefab => _gameDataProvider.GameConfig.TankUnitPrefab;

		private Dictionary<int, NetTankUnit> _spawnedTanks = new();

		public void Initialize()
		{
			if (_netModeProvider.NetMode != ENetMode.Client)
				return;

			NetworkClient.RegisterSpawnHandler(TankUnitPrefab.netId, ClientSpawn, ClientUnspawn);
		}

		#region Server

		public NetTankUnit ServerSpawn(NetworkConnectionToClient conn)
		{
			int id = conn.connectionId;
			
			TankFactory.Args args = new TankFactory.Args
			{
				IsOwned = conn is LocalConnectionToClient
			};
			
			NetTankUnit tank = _tankFactory.Create(args);
			Setup(tank, id);

			_spawnedTanks[id] = tank;

			return tank;
		}

		public void ServerUnpsawn(NetworkConnectionToClient conn)
		{
			int id = conn.connectionId;

			if (_spawnedTanks.TryGetValue(id, out var tank))
			{
				_spawnedTanks.Remove(id);
				NetworkServer.Destroy(tank.gameObject);
			}
		}

		#endregion

		#region Client

		private GameObject ClientSpawn(SpawnMessage msg)
		{
			throw new System.NotImplementedException();
		}

		private void ClientUnspawn(GameObject spawned)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		private void Setup(NetTankUnit tank, int id)
		{
			tank.gameObject.name = $"Tank ({id})";
		}
	}
}