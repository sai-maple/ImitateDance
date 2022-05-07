using System;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class AnglePresenter : IInitializable, IDisposable
    {
        private readonly MusicEntity _musicEntity = default;
        private readonly TurnPlayerEntity _turnPlayerEntity = default;
        private readonly AngleView _angleView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public AnglePresenter(MusicEntity musicEntity, TurnPlayerEntity turnPlayerEntity, AngleView angleView)
        {
            _musicEntity = musicEntity;
            _turnPlayerEntity = turnPlayerEntity;
            _angleView = angleView;
        }

        public void Initialize()
        {
            _musicEntity.OnFinishAsObservable()
                .Subscribe(_ => _angleView.Reset())
                .AddTo(_disposable);

            _turnPlayerEntity.OnChangeAsObservable()
                .Subscribe(_angleView.DoMove)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}