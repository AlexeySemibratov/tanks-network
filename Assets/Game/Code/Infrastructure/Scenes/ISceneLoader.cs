using System.Collections;
using UnityEngine.SceneManagement;

namespace Game.Code.Infrastructure.Scenes
{
	public interface ISceneLoader
	{
		IEnumerator LoadAsync(string scenePath, LoadSceneMode mode, bool makeActive = true);

		IEnumerator UnloadAsync(string scenePath);
	}
}