using System;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class BgmVolumePresenter : IInitializable, IDisposable
    {
        private readonly AudioView _audioView = default;
        private readonly VolumeEntity _volumeEntity = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public BgmVolumePresenter(AudioView audioView, VolumeEntity volumeEntity)
        {
            _audioView = audioView;
            _volumeEntity = volumeEntity;
        }

        public void Initialize()
        {
            _volumeEntity.OnBgmChangeAsObservable()
                .Subscribe(_audioView.OnChangeVolume)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}