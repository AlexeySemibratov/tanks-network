using System.Collections;
using UnityEngine;

namespace Game.Code.Infrastructure.Coroutines
{
	public interface ICoroutineContext
	{
		Coroutine StartCoroutine(IEnumerator cor);
		
		void StopCoroutine(Coroutine cor);
	}
}