namespace Game.Code.Tanks.Models
{
	public class TankInputModel
	{
		public float MoveInputValue { get; set; }
		public float RotateInputValue { get; set; }
		public bool BrakePressed { get; set; }

		public float BrakeInputValue => BrakePressed ? BrakeMax : BrakeMin;

		private const float BrakeMin = 0f;
		private const float BrakeMax = 1f;
	}
}