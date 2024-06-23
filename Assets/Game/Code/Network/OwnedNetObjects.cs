using Game.Code.Tanks;
using UniRx;

namespace Game.Code.Network
{
	public class OwnedNetObjects
	{
		public readonly ReactiveProperty<INetTankUnit> NetTankUnit = new();
	}
}