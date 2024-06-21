using Zenject;

namespace Game.Code.Tanks.Factory
{
	public interface ITankFactory : IFactory<TankFactory.Args, NetTankUnit>
	{
		
	}
}