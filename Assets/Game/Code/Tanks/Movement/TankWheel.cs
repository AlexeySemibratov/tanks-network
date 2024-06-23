using UnityEngine;

namespace Game.Code.Tanks.Movement
{
	public class TankWheel : MonoBehaviour
	{
		public bool IsGrounded => _wheelCollider.isGrounded;
		
		[field:SerializeField] private WheelCollider _wheelCollider;
		
		public void SetMotorTorque(float torque) => _wheelCollider.motorTorque = torque;

		public void SetBrakeTorque(float brake) => _wheelCollider.brakeTorque = brake;
	}
}