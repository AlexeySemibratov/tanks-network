using Cinemachine;
using Zenject;

namespace Game.Code.Tanks.Camera
{
	public class TankOwnedCamera : IInitializable
	{
		[Inject] private CinemachineVirtualCamera _cinemachineCamera;
		[Inject] private TankUnitView _tankView;
		[Inject] private INetTankUnit _netTankUnit;
		
		public void Initialize()
		{
			var transform = _tankView.transform;
			
			_cinemachineCamera.Follow = transform;
			_cinemachineCamera.LookAt = transform;
		}
	}
}