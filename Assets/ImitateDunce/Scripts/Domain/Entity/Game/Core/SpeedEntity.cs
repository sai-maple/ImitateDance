using System;
using UniRx;
using UnityEngine;

namespace ImitateDunce.Domain.Entity.Game.Core
{
    public sealed class SpeedEntity : IDisposable
    {
        private readonly ReactiveProperty<float> _speed = default;
        public float Speed => _speed.Value;

        private readonly float _tmp = default;
        private float _current = default;

        public SpeedEntity()
        {
            _speed = new ReactiveProperty<float>(1);
            _tmp = 120;
            _current = _tmp;
        }

        public void SpeedUp()
        {
            _current += 10;
            _speed.Value = Mathf.Clamp(_current / _tmp, 1, 2);
        }

        public void Dispose()
        {
            _speed?.Dispose();
        }
    }
}