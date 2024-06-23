using UnityEngine;

namespace Game.Code.Infrastructure.Coroutines
{
	public class CoroutineContext : MonoBehaviour, ICoroutineContext
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
}