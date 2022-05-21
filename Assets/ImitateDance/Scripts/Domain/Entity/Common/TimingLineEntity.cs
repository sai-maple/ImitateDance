using System;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class TimingLineEntity : IDisposable
    {
        private readonly ReactiveProperty<float> _lineNormalizePosition = default;
        private readonly Subject<float> _fireLineNormalizePosition = default;

        private const double BarTime = 3f;

        // 初期位置を中央にしたいので、BarTime / 2　から開始する
        private double _time = 1.5f;
        private int _count = default;

        public TimingLineEntity()
        {
            _lineNormalizePosition = new ReactiveProperty<float>();
            _fireLineNormalizePosition = new Subject<float>();
        }

        public IObservable<float> OnLinePositionAsObservable()
        {
            return _lineNormalizePosition;
        }

        public IObservable<float> OnFirePositionAsObservable()
        {
            return _fireLineNormalizePosition.Share();
        }

        public void Fire(float offset)
        {
            var tapTime = _time + offset;
            // + BarTime / 2
            var noteTime = BarTime * _count + 1.5f;
            var diff = tapTime - noteTime;
            var normalize = (diff / (BarTime / 2f));

            _fireLineNormalizePosition.OnNext((float)normalize);
        }

        public void FixUpdate(float deltaTime, float offset)
        {
            _time += deltaTime;
            _count = (int)((_time + offset) / BarTime);

            var position = (_time + offset) % BarTime - (BarTime / 2f);
            _lineNormalizePosition.Value = (float)(position / (BarTime / 2f));
        }

        public void Dispose()
        {
            _lineNormalizePosition?.Dispose();
            _fireLineNormalizePosition?.Dispose();
        }
    }
}