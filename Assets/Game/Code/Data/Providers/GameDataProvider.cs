using Game.Code.Data.Configs;
using Zenject;

namespace Game.Code.Data.Providers
{
	public class GameDataProvider : IGameDataProvider
	{
		public GameConfig GameConfig => _globalConfig.GameConfig;

		[Inject] private GlobalConfig _globalConfig;
	}
}