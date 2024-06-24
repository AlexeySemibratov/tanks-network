using Game.Code.Data.Configs;
using Game.Code.Data.Providers;
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
		public bool IsOwned { get; set; }

		[Inject] private INetModeProvider _netModeProvider;
		[Inject] private IGameDataProvider _gameDataProvider;
		
		public override void InstallBindings()
		{
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
				.BindInterfacesAndSelfTo<TankUnitView>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.Bind<TankConfig>()
				.FromInstance(_gameDataProvider.TankConfig);
		}

		private void InstallModels()
		{
			Container
				.Bind<TankMovementModel>()
				.AsSingle();
			
			Container
				.Bind<TankInputModel>()
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
				.BindInterfacesTo<TankMovementController>()
				.AsSingle();
			
			Container
				.Bind<TankResetController>()
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