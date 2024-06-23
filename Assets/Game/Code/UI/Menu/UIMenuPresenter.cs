using System.Collections;
using Game.Code.Data.Providers;
using Game.Code.Infrastructure.Coroutines;
using Game.Code.Infrastructure.Scenes;
using Game.Code.Infrastructure.UI;
using Game.Code.Network;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Code.UI.Menu
{
	public class UIMenuPresenter : UiBasePresenter<IUIMenuView>
	{
		[Inject] private INetModeInitializer _netModeInitializer;
		[Inject] private ISceneLoader _sceneLoader;
		[Inject] private IGameDataProvider _gameDataProvider;
		[Inject] private ICoroutineContext _coroutineContext;

		protected override void OnViewShowed()
		{
			base.OnViewShowed();

			View.StartHostClicked
				.Subscribe(_ => StartNetMode(ENetMode.Host))
				.AddTo(HideDisposables);
			
			View.StartClientClicked
				.Subscribe(_ => StartNetMode(ENetMode.Client))
				.AddTo(HideDisposables);
		}

		private void StartNetMode(ENetMode mode)
		{
			_netModeInitializer.SetNetMode(mode);
			
			string gameplayScene = _gameDataProvider.ScenesConfig.Gameplay;
			IEnumerator loadCor = _sceneLoader.LoadAsync(gameplayScene, LoadSceneMode.Single);

			_coroutineContext.StartCoroutine(loadCor);
		}
	}
}