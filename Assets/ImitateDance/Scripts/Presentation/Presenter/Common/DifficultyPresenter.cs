using System;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class DifficultyPresenter : IInitializable, IDisposable
    {
        private readonly DifficultyEntity _difficultyEntity = default;
        private readonly DifficultyView _difficultyView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public DifficultyPresenter(DifficultyEntity difficultyEntity, DifficultyView difficultyView)
        {
            _difficultyEntity = difficultyEntity;
            _difficultyView = difficultyView;
        }

        public void Initialize()
        {
            _difficultyView.SetDifficult(_difficultyEntity.Value);

            _difficultyEntity.OnChangeAsObservable()
                .Subscribe(_difficultyView.SetDifficult)
                .AddTo(_disposable);

            _difficultyView.OnNextAsObservable()
                .Subscribe(_ => _difficultyEntity.Next())
                .AddTo(_disposable);

            _difficultyView.OnPreviousAsObservable()
                .Subscribe(_ => _difficultyEntity.Previous())
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}