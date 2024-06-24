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

		public static IObservable<bool> PerformedAsButton(this InputAction action)
			=> Observable.Merge(
				PerformedAsObservable(action).Select(_ => true),
				CancelledAsObservable(action).Select(_ => false)
			);

		public static void Set<T>(this IReactiveProperty<T> source, T value) => source.Value = value;
		public static void Set(this FloatReactiveProperty source, float value) => source.Value = value;
		public static void Set(this IntReactiveProperty source, int value) => source.Value = value;
		public static void Set(this BoolReactiveProperty source, bool value) => source.Value = value;
	}
}