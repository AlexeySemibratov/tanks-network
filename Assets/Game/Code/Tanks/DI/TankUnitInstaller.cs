using Game.Code.Network;
using Game.Code.Tanks.Camera;
using Game.Code.Tanks.Input;
using Game.Code.Tanks.Models;
using Game.Code.Tanks.Movement;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.DI
{
	public class TankUnitInstaller : MonoInstaller
	{
		public bool IsOwned;

		[Inject] private INetModeProvider _netModeProvider;

		public override void InstallBindings()
		{
			Debug.Log($"Is owned {IsOwned}");

			InstallCommon();
			InstallModels();
			InstallComponents();

			if (_netModeProvider.NetMode.IsServer())
				InstallServer();

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

		private void InstallComponents()
		{
			Container
				.Bind<Rigidbody>()
				.FromComponentInHierarchy()
				.AsSingle();
		}

		private void InstallServer()
		{
			Container
				.BindInterfacesTo<TankMovement>()
				.AsSingle();
		}

		private void InstallOwned()
		{
			Container
				.BindInterfacesTo<TankOwnedCamera>()
				.AsSingle();

			Container
				.BindInterfacesTo<TankOwnedInput>()
				.AsSingle();
		}
	}
}