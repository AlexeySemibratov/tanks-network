using Game.Code.Data.Providers;
using Game.Code.Network;
using Game.Code.Tanks.DI;
using Mirror;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Factory
{
	public class TankFactory : ITankFactory
	{
		public struct Args
		{
			public bool IsOwned;

			public Vector3 Position;
			public Quaternion Rotation;
			
			// Server only
			public int ServerId;

			// Client only
			public SpawnMessage ClientSpawnMessage;
		}

		[Inject] private IGameDataProvider _gameData;
		[Inject] private INetModeProvider _netModeProvider;
		[Inject] private DiContainer _diContainer;

		public NetTankUnit Create(Args args)
		{
			NetTankUnit prefab = _gameData.GamePrefabsConfig.TankUnitPrefab;
			NetTankUnit instance = Object.Instantiate(prefab, args.Position, args.Rotation);
			
			Setup(instance, args);
			
			return instance;
		}
		
		private void Setup(NetTankUnit tank, Args args)
		{
			if (_netModeProvider.NetMode.IsServer())
				tank.ServerInit(args.ServerId);
			else
				NetworkClient.ApplySpawnPayload( tank.netIdentity, args.ClientSpawnMessage );
			
			InitZenject(tank, args);

			tank.gameObject.name = $"Tank ({tank.Id})";
		}

		private void InitZenject(NetTankUnit tank, Args args)
		{
			TankUnitInstaller installer = tank.GetComponent<TankUnitInstaller>();
			installer.IsOwned = args.IsOwned;

			GameObjectContext goContext = tank.GetComponent<GameObjectContext>();

			goContext.Construct(_diContainer);
		}
	}
}