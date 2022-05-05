using System;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class VolumeEditPresenter : IInitializable, IDisposable
    {
        private readonly VolumeEntity _volumeEntity = default;
        private readonly VolumeSliderView _volumeSliderView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public VolumeEditPresenter(VolumeEntity volumeEntity, VolumeSliderView volumeSliderView)
        {
            _volumeEntity = volumeEntity;
            _volumeSliderView = volumeSliderView;
        }

        public void Initialize()
        {
            _volumeSliderView.Initialize(_volumeEntity.SeVolume, _volumeEntity.BgmVolume);
            _volumeSliderView.OnBgmEditAsObservable()
                .Subscribe(_volumeEntity.SetBgmVolume)
                .AddTo(_disposable);

            _volumeSliderView.OnSeEditEndAsObservable()
                .Subscribe(_volumeEntity.SetSeVolume)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}