using System;
using ImitateDunce.Domain.Entity.Game.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.Core
{
    public sealed class TimePresenter : IInitializable, IDisposable
    {
        private readonly TimeEntity _timeEntity = default;
        private readonly SpeedEntity _speedEntity = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TimePresenter(TimeEntity timeEntity, SpeedEntity speedEntity)
        {
            _timeEntity = timeEntity;
            _speedEntity = speedEntity;
        }

        public void Initialize()
        {
            Observable.EveryFixedUpdate()
                .Subscribe(_ => _timeEntity.FixUpdate(Time.deltaTime * _speedEntity.Speed))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}