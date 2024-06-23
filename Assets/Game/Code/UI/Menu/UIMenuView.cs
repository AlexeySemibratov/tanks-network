using System;
using Game.Code.Infrastructure.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Code.UI.Menu
{
	public class UIMenuView : UIBaseView, IUIMenuView
	{
		public IObservable<Unit> StartHostClicked => _startHostButton.OnClickAsObservable();
		public IObservable<Unit> StartClientClicked => _startClientButton.OnClickAsObservable();

		[SerializeField] private Button _startHostButton;
		[SerializeField] private Button _startClientButton;
	}
}