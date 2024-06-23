using Cinemachine;
using UnityEngine;
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
			Debug.Log($"Init tank unit {_netTankUnit.Id}");
			var transform = _tankView.transform;
			
			_cinemachineCamera.Follow = transform;
			_cinemachineCamera.LookAt = transform;
		}
	}
}