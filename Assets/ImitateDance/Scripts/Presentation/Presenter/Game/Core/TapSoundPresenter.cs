using System;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class TapSoundPresenter : IInitializable, IDisposable
    {
        private readonly ScoreEntity _scoreEntity = default;
        private readonly AudioSource _audioSource = default;
        private readonly AudioClip _audioClip = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TapSoundPresenter(ScoreEntity scoreEntity, AudioSource audioSource, AudioClip audioClip)
        {
            _scoreEntity = scoreEntity;
            _audioSource = audioSource;
            _audioClip = audioClip;
        }

        public void Initialize()
        {
            _scoreEntity.OnDanceAsObservable()
                .Subscribe(_ => _audioSource.PlayOneShot(_audioClip))
                .AddTo(_disposable);

            _scoreEntity.OnDanceAsObservable()
                .Subscribe(_ => _audioSource.PlayOneShot(_audioClip))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}