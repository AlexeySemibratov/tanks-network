using Game.Code.Tanks;
using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Game Prefabs Config", menuName = AssetMenus.General + "Game Prefabs Config")]
	public class GamePrefabsConfig : ScriptableObject
	{
		public NetTankUnit TankUnitPrefab;
	}
}