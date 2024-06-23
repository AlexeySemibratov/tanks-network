using Game.Code.Tanks.Movement;
using UnityEngine;

namespace Game.Code.Tanks
{
	public class TankUnitView : MonoBehaviour
	{
		[field:SerializeField] public TankWheel[] WheelsL { get; private set; }
		[field:SerializeField] public TankWheel[] WheelsR { get; private set; }
		
		[field:SerializeField] public TankWheel MiddleWheelL { get; private set; }
		[field:SerializeField] public TankWheel MiddleWheelR { get; private set; }

		private void OnValidate()
		{
			MiddleWheelL = WheelsL[WheelsL.Length / 2];
			MiddleWheelR = WheelsR[WheelsR.Length / 2];
		}
	}
}