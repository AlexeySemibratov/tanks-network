using Zenject;

namespace Game.Code.Infrastructure.UI.DI
{
	public static class UIBinder
	{
		public static void BindView<TView>(this DiContainer container)
		{
			container
				.BindInterfacesTo<TView>()
				.FromComponentsInHierarchy()
				.AsSingle();
		}
        
		public static void BindPresenter<TPresenter>(this DiContainer container)
		{
			container
				.BindInterfacesAndSelfTo<TPresenter>()
				.AsSingle();
		}
	}
}