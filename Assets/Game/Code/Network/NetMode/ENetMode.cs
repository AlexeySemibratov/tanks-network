namespace Game.Code.Network
{
	public enum ENetMode
	{
		Client,
		Server,
		Host
	}
	
	public static class ENetModeExt
	{
		public static bool IsServer(this ENetMode mode) => mode is ENetMode.Server or ENetMode.Host;
		
		public static bool IsClient(this ENetMode mode) => mode is ENetMode.Client or ENetMode.Host;
	}
}