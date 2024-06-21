namespace Game.Code.Network
{
	public class NetModeProvider : INetModeProvider
	{
		public ENetMode NetMode => ENetMode.Host;
	}
}