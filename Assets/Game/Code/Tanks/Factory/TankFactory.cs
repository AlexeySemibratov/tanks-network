using Game.Code.Data.Providers;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Factory
{
	public class TankFactory : ITankFactory
	{
		public struct Args
		{
			public bool IsOwned;
		}

		[Inject] private IGameDataProvider _gameData;

		public NetTankUnit Create(Args args)
		{
			NetTankUnit prefab = _gameData.GameConfig.TankUnitPrefab;
			NetTankUnit instance = Object.Instantiate(prefab);

			return instance;
		}
		
		// NetTankUnit Construct( GameObjectContext goContext, int id, bool isOwned = false )
		// {
		// 	goContext.name			= $"{player.name} [id={id}]";
		//
		// 	_diContainer.BindInstance( isOwned );
		// 	
		// 	goContext.Construct( _diContainer );
		//
		// 	NetTankUnit resolvedInstance = goContext.Container.Resolve< NetTankUnit >();
		// 	
		// 	_diContainer.Unbind<bool>();
		//
		// 	return playerInstance;
		// }
	}
}