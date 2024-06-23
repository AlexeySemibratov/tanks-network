using Game.Code.UI.Menu;
using UnityEngine;
using Zenject;

namespace Game.Code.Infrastructure.Scenes.Bootstraps
{
	public class MenuBootstrap : MonoBehaviour
	{
		[Inject] private IUIMenuView _menuView;
		
		private void Start()
		{
			_menuView.Show();
		}
	}
}