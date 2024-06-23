using Cinemachine;
using Game.Code.Infrastructure.UI.DI;
using Game.Code.Input;
using Game.Code.Network;
using Game.Code.Network.Spawners;
using Game.Code.Tanks.Factory;
using Game.Code.UI.HUD;
using UnityEngine;
using Zenject;

namespace Game.Code.Infrastructure.Scenes
{
	public class GameplaySceneInstaller : MonoInstaller
	{
		[Inject] private INetModeProvider _netModeProvider;
		
		public override void InstallBindings()
		{
			InstallSpawners();
			InstallFactories();
			
			if (_netModeProvider.NetMode.IsClient())
				InstallClient();
		}

		private void InstallSpawners()
		{
			Container
				.BindInterfacesTo<NetTankSpawner>()
				.AsSingle();
		}
		
		private void InstallFactories()
		{
			Container
				.BindInterfacesTo<TankFactory>()
				.AsSingle();
		}

		private void InstallClient()
		{
			InstallInput();
			InstallCameras();
			InstallUI();
			InstallClientNetwork();
		}

		private void InstallInput()
		{
			Container
				.BindInterfacesTo<PlayerInput>()
				.AsSingle();
		}

		private void InstallCameras()
		{
			Container
				.Bind<CinemachineVirtualCamera>()
				.FromComponentInHierarchy()
				.AsSingle();
			
			Container
				.Bind<Camera>()
				.FromComponentInHierarchy()
				.AsSingle();
		}

		private void InstallUI()
		{
			Container.BindView<UIGameHudView>();
			Container.BindPresenter<UIGameHudPresenter>();
		}

		private void InstallClientNetwork()
		{
			Container
				.Bind<OwnedNetObjects>()
				.AsSingle();
		}
	}
}