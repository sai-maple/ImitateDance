using System;
using ImitateDance.Scripts.Domain.Entity.Common;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class SeVolumePresenter : IInitializable, IDisposable
    {
        private readonly AudioSource _audioSource = default;
        private readonly VolumeEntity _volumeEntity = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public SeVolumePresenter(AudioSource audioSource, VolumeEntity volumeEntity)
        {
            _audioSource = audioSource;
            _volumeEntity = volumeEntity;
        }

        public void Initialize()
        {
            _volumeEntity.OnSeChangeAsObservable()
                .Subscribe(value => _audioSource.volume = value)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}