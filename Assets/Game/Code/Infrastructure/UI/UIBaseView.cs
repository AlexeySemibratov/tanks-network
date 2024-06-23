using System;
using UniRx;
using UnityEngine;

namespace Game.Code.Infrastructure.UI
{
	public class UIBaseView : MonoBehaviour, IUIView
	{
		public IObservable<Unit> Showed => _showedSubject;

		public IObservable<Unit> Hided => _hidedSubject;

		private Subject<Unit> _showedSubject = new();
		private Subject<Unit> _hidedSubject = new();

		protected virtual void OnEnable() => _showedSubject.OnNext(Unit.Default);

		protected virtual void OnDisable() => _hidedSubject.OnNext(Unit.Default);

		public virtual void Show() => gameObject.SetActive(true);
        
		public virtual void Hide() => gameObject.SetActive(false);
	}
}