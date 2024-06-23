using System;
using UniRx;

namespace Game.Code.Infrastructure.UI
{
	public interface IUIView
	{
		IObservable<Unit> Showed { get; }
		IObservable<Unit> Hided { get; }

		void Show();
		void Hide();
	}
}