using System;
using UniRx;
using UnityEngine.InputSystem;

namespace Game.Code.Extensions
{
	public static class RxExtensions
	{
		public static IObservable<InputAction.CallbackContext> PerformedAsObservable(this InputAction action)
			=>
				Observable.FromEvent<InputAction.CallbackContext>(
					h => action.performed += h,
					h => action.performed -= h
				);
		
		public static IObservable<InputAction.CallbackContext> CancelledAsObservable(this InputAction action)
			=>
				Observable.FromEvent<InputAction.CallbackContext>(
					h => action.canceled += h,
					h => action.canceled -= h
				);
	}
}