using Game.Code.Tanks;
using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Game Config", menuName = AssetMenus.General + "Game Config")]
	public class GameConfig : ScriptableObject
	{
		public NetTankUnit TankUnitPrefab;
	}
}