using Game.Code.Infrastructure.UI.DI;
using Game.Code.UI.Menu;
using Zenject;

namespace Game.Code.Infrastructure.Scenes
{
	public class MenuSceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindMenuUI();
		}

		private void BindMenuUI()
		{
			Container.BindView<UIMenuView>();
			Container.BindPresenter<UIMenuPresenter>();
		}
	}
}