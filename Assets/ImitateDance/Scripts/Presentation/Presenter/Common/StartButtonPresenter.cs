using System;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UnityEngine.UI;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class StartButtonPresenter : IInitializable, IDisposable
    {
        private readonly Button _button = default;
        private readonly AudioView _audioView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public StartButtonPresenter(Button button, AudioView audioView)
        {
            _button = button;
            _audioView = audioView;
        }

        public void Initialize()
        {
            _button.OnClickAsObservable()
                .Subscribe(_ => _audioView.StopAsync().Forget())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}