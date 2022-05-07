using System;
using UniRx;
using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class SpeedEntity : IDisposable
    {
        private readonly ReactiveProperty<float> _speed = default;
        public float Speed => _speed.Value;

        private readonly float _tmp = default;

        public SpeedEntity()
        {
            _speed = new ReactiveProperty<float>(1);
            _tmp = 120;
        }

        public IObservable<float> OnChangeAsObservable()
        {
            return _speed;
        }

        public void SetBpm(int bpm)
        {
            _speed.Value = Mathf.Clamp(bpm / _tmp, 1, 2);
        }

        public void Dispose()
        {
            _speed?.Dispose();
        }
    }
}