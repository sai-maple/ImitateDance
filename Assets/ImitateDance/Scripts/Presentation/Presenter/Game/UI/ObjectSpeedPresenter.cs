using System;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class ObjectSpeedPresenter : IInitializable, IDisposable
    {
        private readonly SpeedEntity _speedEntity = default;
        private readonly Animator _animator = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private static readonly int Ideal = Animator.StringToHash("Ideal");

        public ObjectSpeedPresenter(SpeedEntity speedEntity, Animator animator)
        {
            _speedEntity = speedEntity;
            _animator = animator;
        }

        public void Initialize()
        {
            _speedEntity.OnChangeAsObservable()
                .Subscribe(speed =>
                {
                    _animator.SetTrigger(Ideal);
                    _animator.speed = speed;
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}