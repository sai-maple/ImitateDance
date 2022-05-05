using System;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class TimePresenter : IInitializable, IDisposable
    {
        private readonly TimeEntity _timeEntity = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TimePresenter(TimeEntity timeEntity)
        {
            _timeEntity = timeEntity;
        }

        public void Initialize()
        {
            Observable.EveryFixedUpdate()
                .Subscribe(_ => _timeEntity.FixUpdate(Time.deltaTime))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}