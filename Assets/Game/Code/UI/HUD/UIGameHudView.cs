using Game.Code.Infrastructure.UI;
using TMPro;
using UnityEngine;

namespace Game.Code.UI.HUD
{
	public class UIGameHudView : UIBaseView, IUIGameHudView
	{
		[SerializeField] private TextMeshProUGUI _velocityText;
		
		public void SetVelocityText(string text)
		{
			_velocityText.text = text;
		}
	}
}