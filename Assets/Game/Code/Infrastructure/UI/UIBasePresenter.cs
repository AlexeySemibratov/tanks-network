using System;
using UniRx;
using Zenject;

namespace Game.Code.Infrastructure.UI
{
	public abstract class UiBasePresenter<T> : IInitializable, IDisposable
		where T : IUIView
	{
		[Inject] protected T View;

		protected readonly CompositeDisposable HideDisposables = new();
		protected readonly CompositeDisposable LifetimeDisposable = new();

		public virtual void Initialize()
		{
			View.Showed
				.Subscribe(_ => OnViewShowed())
				.AddTo(LifetimeDisposable);
			
			View.Hided
				.Subscribe(_ => OnViewHided())
				.AddTo(LifetimeDisposable);
		}

		public virtual void Dispose()
		{
			HideDisposables.Dispose();
			LifetimeDisposable.Dispose();
		}

		protected virtual void OnViewShowed()
		{
		}

		protected virtual void OnViewHided()
		{
			HideDisposables.Clear();
		}
	}
}