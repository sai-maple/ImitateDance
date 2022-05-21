using System;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UniScreen.Container;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class TimingConfigPresenter : IInitializable, IDisposable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly TimingConfigEntity _timingConfigEntity = default;
        private readonly TimingConfigView _timingConfigView = default;
        private readonly AudioView _audioView = default;
        private readonly CloseScreenButton _closeButton = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TimingConfigPresenter(ScreenContainer screenContainer, TimingConfigEntity timingConfigEntity,
            TimingConfigView timingConfigView, AudioView audioView, CloseScreenButton closeButton)
        {
            _screenContainer = screenContainer;
            _timingConfigEntity = timingConfigEntity;
            _timingConfigView = timingConfigView;
            _audioView = audioView;
            _closeButton = closeButton;
        }

        public void Initialize()
        {
            _audioView.StopAsync().Forget();
            _timingConfigEntity.OnTimingChangeAsObservable()
                .Subscribe(_timingConfigView.OnChanged)
                .AddTo(_disposable);

            _timingConfigView.OnUpAsObservable()
                .Subscribe(_ => _timingConfigEntity.Plus())
                .AddTo(_disposable);

            _timingConfigView.OnDownAsObservable()
                .Subscribe(_ => _timingConfigEntity.Minus())
                .AddTo(_disposable);

            _closeButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _audioView.Play();
                    _screenContainer.Pop().Forget();
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}