using System;
using Game.Code.Extensions;
using Game.Code.Input;
using UniRx;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Code.Tanks.Input
{
	public class TankOwnedInput : IInitializable, IDisposable
	{
		[Inject] private IPlayerInput _playerInput;
		[Inject] private INetTankUnit _netTankUnit;

		private CompositeDisposable _disposables = new();

		public void Initialize()
			=> HandleTankControlActions(_playerInput.TankControls);

		public void Dispose()
			=> _disposables.Dispose();

		private void HandleTankControlActions(Controls.TankControlActions actions)
		{
			HandleInputAxis(actions.Move, true);
			HandleInputAxis(actions.Rotate, false);

			actions.Shoot
				.PerformedAsObservable()
				.Subscribe(_ => _netTankUnit.CmdShoot())
				.AddTo(_disposables);
		}

		private void HandleInputAxis(InputAction action, bool isMoveAxis)
		{
			Observable.Merge(
					action
						.PerformedAsObservable()
						.Select(context => context.ReadValue<float>()),
					action
						.CancelledAsObservable()
						.Select(_ => 0f)
				)
				.Subscribe(value =>
				{
					// Debug.Log($"Move input {value}");

					if (isMoveAxis)
						_netTankUnit.CmdMoveAxisInput(value);
					else
						_netTankUnit.CmdRotateAxisInput(value);
				})
				.AddTo(_disposables);
		}
	}
}