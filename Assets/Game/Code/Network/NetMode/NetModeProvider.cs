namespace Game.Code.Network
{
	public class NetModeProvider : INetModeProvider, INetModeInitializer
	{
		public ENetMode NetMode => _netMode;

		private ENetMode _netMode;
		
		public void SetNetMode(ENetMode mode) => _netMode = mode;
	}
}