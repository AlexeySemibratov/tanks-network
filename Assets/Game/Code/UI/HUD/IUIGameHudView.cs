using Game.Code.Infrastructure.UI;

namespace Game.Code.UI.HUD
{
	public interface IUIGameHudView : IUIView
	{
		void SetVelocityText(string text);
	}
}