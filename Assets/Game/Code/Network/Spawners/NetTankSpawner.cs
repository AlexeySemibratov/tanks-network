using System.Collections.Generic;
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
		
		// Client/Host only
		[InjectOptional] private OwnedNetObjects _ownedNetObjects;

		private NetTankUnit TankUnitPrefab => _gameDataProvider.GamePrefabsConfig.TankUnitPrefab;

		private Dictionary<int, NetTankUnit> _spawnedTanks = new();

		public void Initialize()
		{
			if (_netModeProvider.NetMode != ENetMode.Client)
				return;

			NetworkClient.RegisterSpawnHandler(TankUnitPrefab.AssetNetIdentity.assetId, ClientSpawn, ClientUnspawn);
		}

		#region Server


		public NetTankUnit ServerSpawn(NetworkConnectionToClient conn, Transform point)
		{
			int id = conn.connectionId;
			bool isOwned = conn is LocalConnectionToClient;
			
			TankFactory.Args args = new TankFactory.Args
			{
				ServerId = id,
				IsOwned = isOwned,
				
				Position = point.position,
				Rotation = point.rotation
			};
			
			NetTankUnit tank = _tankFactory.Create(args);

			_spawnedTanks[id] = tank;
			
			SetOwnedTank(tank, isOwned);

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
			TankFactory.Args args = new TankFactory.Args
			{
				IsOwned = msg.isLocalPlayer,
				
				Position = msg.position,
				Rotation = msg.rotation
			};
			
			NetTankUnit tank = _tankFactory.Create(args);

			SetOwnedTank(tank, msg.isLocalPlayer);

			return tank.gameObject;
		}

		private void ClientUnspawn(GameObject spawned)
		{
			Object.Destroy(spawned);
		}

		#endregion

		private void SetOwnedTank(NetTankUnit tank, bool isOwned)
		{
			if (isOwned)
				_ownedNetObjects.NetTankUnit.Value = tank;
		}
	}
}