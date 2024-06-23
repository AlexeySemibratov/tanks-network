using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Scenes Config", menuName = AssetMenus.Configs + "Scenes Config")]
	public class ScenesConfig : ScriptableObject
	{
		public string Gameplay;
		public string Menu;
	}
}