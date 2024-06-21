using System;
using Game.Code.Input;
using UnityEngine;
using Zenject;

namespace Game.Code.Tanks.Movement
{
	public class TankOwnedMovement : IInitializable, IDisposable
	{
		[Inject] private Rigidbody _rigidbody;
		[Inject] private IPlayerInput _input;
		
		public void Initialize()
		{
			
		}
		
		public void Dispose()
		{
			
		}
	}
}