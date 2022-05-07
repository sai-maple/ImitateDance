using System;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class CpuPresenter : IInitializable, IDisposable
    {
        private readonly InputUseCase _inputUseCase = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public CpuPresenter(InputUseCase inputUseCase)
        {
            _inputUseCase = inputUseCase;
        }

        public void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => _inputUseCase.CpuInput())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}