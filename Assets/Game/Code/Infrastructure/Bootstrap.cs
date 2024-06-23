using Game.Code.Data.Providers;
using Game.Code.Infrastructure.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Code.Infrastructure
{
	public class Bootstrap : MonoBehaviour
	{
		[Inject] private ISceneLoader _sceneLoader;
		[Inject] private IGameDataProvider _gameDataProvider;
		
		private void Start()
		{
			var menuScene = _gameDataProvider.ScenesConfig.Menu;

			StartCoroutine(_sceneLoader.LoadAsync(menuScene, LoadSceneMode.Single));
		}
	}
}