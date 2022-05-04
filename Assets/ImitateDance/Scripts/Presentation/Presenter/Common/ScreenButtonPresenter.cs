using System;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UniScreen.Container;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class ScreenButtonPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly ScreenButton _screenButton = default;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public ScreenButtonPresenter(ScreenContainer screenContainer, ScreenButton screenButton)
        {
            _screenContainer = screenContainer;
            _screenButton = screenButton;
        }

        public void Initialize()
        {
            _screenButton.OnClickAsObservable()
                .Subscribe(screen => _screenContainer.Push(screen).Forget())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}