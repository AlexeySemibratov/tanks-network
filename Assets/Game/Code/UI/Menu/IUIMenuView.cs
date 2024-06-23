using System;
using Game.Code.Infrastructure.UI;
using UniRx;

namespace Game.Code.UI.Menu
{
	public interface IUIMenuView : IUIView
	{
		IObservable<Unit> StartHostClicked { get; }
		IObservable<Unit> StartClientClicked { get; }
	}
}