using System;
using System.Collections.Generic;
using System.Linq;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class TimingLinePresenter : IInitializable, IDisposable
    {
        private readonly TimingLineEntity _timingLineEntity = default;
        private readonly TimingConfigEntity _timingConfigEntity = default;
        private readonly KeyConfigEntity _keyConfigEntity = default;
        private readonly TimingLineView _timingLineView = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TimingLinePresenter(TimingLineEntity timingLineEntity, TimingConfigEntity timingConfigEntity,
            KeyConfigEntity keyConfigEntity, TimingLineView timingLineView)
        {
            _timingLineEntity = timingLineEntity;
            _timingConfigEntity = timingConfigEntity;
            _keyConfigEntity = keyConfigEntity;
            _timingLineView = timingLineView;
        }

        public void Initialize()
        {
            var list = new List<DanceDirection>()
                { DanceDirection.Down, DanceDirection.Left, DanceDirection.Right, DanceDirection.Up };
            _timingLineView.Initialize(list.Select(direction => _keyConfigEntity.GetKey(direction)));

            Observable.EveryFixedUpdate()
                .Subscribe(_ => _timingLineEntity.FixUpdate(Time.deltaTime, _timingConfigEntity.Value))
                .AddTo(_disposable);

            _timingLineEntity.OnLinePositionAsObservable()
                .Subscribe(_timingLineView.OnUpdateLine)
                .AddTo(_disposable);

            _timingLineEntity.OnFirePositionAsObservable()
                .Subscribe(_timingLineView.OnFire)
                .AddTo(_disposable);

            _timingLineView.OnFireAsObservable()
                .Subscribe(_ => _timingLineEntity.Fire(_timingConfigEntity.Value))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}