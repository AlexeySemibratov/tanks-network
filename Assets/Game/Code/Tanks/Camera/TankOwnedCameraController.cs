using Cinemachine;
using Zenject;

namespace Game.Code.Tanks.Camera
{
	public class TankOwnedCameraController : IInitializable
	{
		[Inject] private CinemachineVirtualCamera _cinemachineCamera;
		[Inject] private TankUnitView _tankView;
		
		public void Initialize()
		{
			_cinemachineCamera.Follow = _tankView.transform;
		}
	}
}