using Game.Code.Tanks.Camera;
using Game.Code.Tanks.Models;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.DI
{
	public class TankUnitInstaller : MonoInstaller
	{
		public bool IsOwned;

		public override void InstallBindings()
		{
			InstallCommon();
			InstallModels();
			InstallMovement();

			if (IsOwned)
				InstallOwned();
		}

		private void InstallCommon()
		{
			Container
				.BindInterfacesTo<NetTankUnit>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.Bind<TankUnitView>()
				.FromComponentInHierarchy()
				.AsSingle();
		}

		private void InstallModels()
		{
			Container
				.Bind<TankMovementModel>()
				.AsSingle();
		}

		private void InstallMovement()
		{
			Container
				.Bind<Rigidbody>()
				.FromComponentInHierarchy()
				.AsSingle();
		}

		private void InstallOwned()
		{
			Container
				.BindInterfacesTo<TankOwnedCameraController>()
				.AsSingle();
		}
	}
}