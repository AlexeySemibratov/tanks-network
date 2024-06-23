using System.Collections;
using UnityEngine.SceneManagement;

namespace Game.Code.Infrastructure.Scenes
{
	public class SceneLoader : ISceneLoader
	{
		public IEnumerator LoadAsync(string scenePath, LoadSceneMode mode, bool makeActive = true)
		{
			yield return SceneManager.LoadSceneAsync(scenePath, mode);
            
			if (makeActive)
				SetActiveScene(scenePath);
		}

		public IEnumerator UnloadAsync(string scenePath)
		{
			yield return SceneManager.UnloadSceneAsync(scenePath);
		}
		
		private void SetActiveScene(string path) => SceneManager.SetActiveScene(GetScene(path));

		private Scene GetScene(string path) => SceneManager.GetSceneByPath(path);
	}
}