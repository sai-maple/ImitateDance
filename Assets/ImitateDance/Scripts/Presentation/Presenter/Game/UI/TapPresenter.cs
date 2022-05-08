using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class TapPresenter : IInitializable, IDisposable
    {
        private readonly TurnPlayerEntity _turnPlayerEntity = default;
        private readonly HideButtonEntity _hideButtonEntity = default;
        private readonly InputUseCase _inputUseCase = default;
        private readonly InputView _inputView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TapPresenter(TurnPlayerEntity turnPlayerEntity, HideButtonEntity hideButtonEntity,
            InputUseCase inputUseCase, InputView inputView)
        {
            _turnPlayerEntity = turnPlayerEntity;
            _hideButtonEntity = hideButtonEntity;
            _inputUseCase = inputUseCase;
            _inputView = inputView;
        }

        public void Initialize()
        {
            _inputView.Active(_hideButtonEntity.IsHide);
            _inputView.OnTapAsObservable()
                .Where(_ => _turnPlayerEntity.Current == TurnPlayer.Self)
                .Subscribe(_inputUseCase.OnTap)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}