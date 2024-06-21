using Game.Code.Data.Configs;

namespace Game.Code.Data.Providers
{
	public interface IGameDataProvider
	{
		GamePrefabsConfig GamePrefabsConfig { get; }
		TankConfig TankConfig { get; }
	}
}