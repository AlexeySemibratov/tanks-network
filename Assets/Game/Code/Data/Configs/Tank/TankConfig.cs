using UnityEngine;

namespace Game.Code.Data.Configs
{
	[CreateAssetMenu(fileName = "Tank Config", menuName = AssetMenus.General + "Tank Config")]
	public class TankConfig : ScriptableObject
	{
		[Header("Engine")]
		public float EngineGasTorque;
		public float EngineRelativeTorqueAcceleration;
		public float EngineBreakTorque;
		
		public float MaxForwardSpeed;
		public float MaxBackwardSpeed;
		public float MaxAngularSpeed;
	}
}