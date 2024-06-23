using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Tank Config", menuName = AssetMenus.General + "Tank Config")]
	public class TankConfig : ScriptableObject
	{
		public float EngineForce;
		public float MaxForwardSpeed;
		public float MaxBackSpeed;
		public float RotationSpeed;
	}
}