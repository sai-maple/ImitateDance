using System;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.UseCase.Game.Core;
using ImitateDunce.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.UI
{
    public sealed class TapPresenter : IInitializable, IDisposable
    {
        private readonly TurnPlayerEntity _turnPlayerEntity = default;
        private readonly InputUseCase _inputUseCase = default;
        private readonly InputView _inputView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TapPresenter(TurnPlayerEntity turnPlayerEntity, InputUseCase inputUseCase, InputView inputView)
        {
            _turnPlayerEntity = turnPlayerEntity;
            _inputUseCase = inputUseCase;
            _inputView = inputView;
        }

        public void Initialize()
        {
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