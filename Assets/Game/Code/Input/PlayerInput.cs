using Zenject;

namespace Game.Code.Input
{
	public class PlayerInput : IPlayerInput, IInitializable
	{
		public Controls.TankControlActions TankControls => _controls.TankControl;
		
		private Controls _controls;
		
		public void Initialize()
		{
			_controls = new Controls();
			
			_controls.Enable();
		}
	}
}