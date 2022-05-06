using System;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class LaneOwnerPresenter : IInitializable, IDisposable
    {
        private readonly TurnPlayerEntity _playerEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly OwnerView _ownerView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public LaneOwnerPresenter(TurnPlayerEntity playerEntity, PhaseEntity phaseEntity, OwnerView ownerView)
        {
            _playerEntity = playerEntity;
            _phaseEntity = phaseEntity;
            _ownerView = ownerView;
        }

        public void Initialize()
        {
            _phaseEntity.OnChangeAsObservable()
                .Subscribe(phase => _ownerView.ChangeState(phase, _playerEntity.Current))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}