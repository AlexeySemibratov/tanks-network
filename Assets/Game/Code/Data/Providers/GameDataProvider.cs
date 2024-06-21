using Game.Code.Data.Configs;
using Zenject;

namespace Game.Code.Data.Providers
{
	public class GameDataProvider : IGameDataProvider
	{
		public GamePrefabsConfig GamePrefabsConfig => _globalConfig.GamePrefabsConfig;
		public TankConfig TankConfig => _globalConfig.TankConfig;

		[Inject] private GlobalConfig _globalConfig;
	}
}