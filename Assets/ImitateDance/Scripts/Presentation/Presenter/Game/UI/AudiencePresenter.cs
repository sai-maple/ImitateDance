using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class AudiencePresenter : IInitializable, IDisposable
    {
        private readonly PointEntity _pointEntity = default;
        private readonly SpeedEntity _speedEntity = default;
        private readonly DifficultyEntity _difficultyEntity = default;
        private readonly AudienceView _audienceView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        private float _threshold = 0;

        public AudiencePresenter(PointEntity pointEntity, SpeedEntity speedEntity, DifficultyEntity difficultyEntity,
            AudienceView audienceView)
        {
            _pointEntity = pointEntity;
            _speedEntity = speedEntity;
            _difficultyEntity = difficultyEntity;
            _audienceView = audienceView;
        }

        public void Initialize()
        {
            _threshold = _difficultyEntity.Value switch
            {
                MusicDifficulty.Easy => 200,
                MusicDifficulty.Normal => 600,
                MusicDifficulty.Hard => 900,
                _ => throw new ArgumentOutOfRangeException()
            };
            _pointEntity.OnSelfPointChangeAsObservable()
                .Subscribe(point => _audienceView.SetSelfPointNormalize(point / _threshold))
                .AddTo(_disposable);

            _pointEntity.OnOpponentPointChangeAsObservable()
                .Subscribe(point => _audienceView.SetOpponentPointNormalize(point / _threshold))
                .AddTo(_disposable);

            _speedEntity.OnChangeAsObservable()
                .Subscribe(_audienceView.SetSpeed)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}