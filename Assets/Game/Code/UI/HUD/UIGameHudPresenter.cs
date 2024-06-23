using System.Globalization;
using Game.Code.Infrastructure.UI;
using Game.Code.Network;
using Game.Code.Tanks;
using Game.Code.Tanks.Models;
using UniRx;
using Zenject;
using CompositeDisposable = UniRx.CompositeDisposable;

namespace Game.Code.UI.HUD
{
	public class UIGameHudPresenter : UiBasePresenter<IUIGameHudView>
	{
		[Inject] private OwnedNetObjects _ownedNetObjects;
		
		private INetTankUnit CurrentTank => _ownedNetObjects.NetTankUnit.Value;
		
		private CompositeDisposable _tankUnitDisposables = new();
		
		private const string BackwardVelocityPrefix = "-";
		private const string VelocityTextFormat = "0.00";
		
		public override void Initialize()
		{
			base.Initialize();

			_ownedNetObjects.NetTankUnit
				.Where(t => t != null)
				.Subscribe(HandleTankUnit)
				.AddTo(LifetimeDisposable);
		}

		public override void Dispose()
		{
			base.Dispose();
			
			_tankUnitDisposables.Dispose();
		}

		private void HandleTankUnit(INetTankUnit tankUnit)
		{
			_tankUnitDisposables.Clear();

			tankUnit.MovementModel.VelocityMagnitude
				.Subscribe(OnVelocityMagnitudeChanged)
				.AddTo(_tankUnitDisposables);
		}

		private void OnVelocityMagnitudeChanged(float velocity)
		{
			var text = velocity.ToString(VelocityTextFormat, CultureInfo.InvariantCulture);
			
			if (CurrentTank.MovementModel.MoveDirection == EMoveDirection.Backward)
				text = text.Insert(0, BackwardVelocityPrefix);

			View.SetVelocityText(text);
		}
	}
}