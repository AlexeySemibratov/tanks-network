using Game.Code.Tanks.Models;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Movement
{
	public class TankResetController
	{
		[Inject] private TankUnitView _tankView;
		[Inject] private TankMovementModel _movementModel;

		private float _lastResetTime = Mathf.NegativeInfinity;

		private const float ResetTimeDebounceSec = 2f;
		private const float ResetVelocityThreshold = 0.2f;

		public void Reset()
		{
			if (ResetNotAllowed())
				return;

			_lastResetTime = Time.time;

			var transform = _tankView.transform;
			var currentPosition = transform.position;

			transform.position = currentPosition + Vector3.up;
			transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
		}

		private bool ResetNotAllowed()
			=>
				_tankView.IsGrounded() ||
				_movementModel.VelocityMagnitude.Value >= ResetVelocityThreshold ||
				Time.time - _lastResetTime < ResetTimeDebounceSec;
	}
}