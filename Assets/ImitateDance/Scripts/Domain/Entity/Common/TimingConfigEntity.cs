using System;
using UniRx;
using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class TimingConfigEntity : IDisposable
    {
        private readonly ReactiveProperty<int> _count = default;
        public float Value { get; private set; }

        private const float Flame = 0.0166667f;

        public TimingConfigEntity()
        {
            _count = new ReactiveProperty<int>();
        }

        public IObservable<int> OnTimingChangeAsObservable()
        {
            return _count;
        }

        public void Plus()
        {
            _count.Value = Mathf.Min(_count.Value + 1, 50);
            Value = _count.Value * Flame;
        }

        public void Minus()
        {
            _count.Value = Mathf.Max(_count.Value - 1, -50);
            Value = _count.Value * Flame;
        }

        public void Dispose()
        {
            _count?.Dispose();
        }
    }
}