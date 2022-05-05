using System;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class VolumeEntity : IDisposable
    {
        private readonly ReactiveProperty<float> _se = default;
        private readonly ReactiveProperty<float> _bgm = default;

        public VolumeEntity()
        {
            _se = new ReactiveProperty<float>(1);
            _bgm = new ReactiveProperty<float>(1);
        }

        public IObservable<float> OnSeChangeAsObservable()
        {
            return _se;
        }
        
        public IObservable<float> OnBgmChangeAsObservable()
        {
            return _bgm;
        }

        public void SetSeVolume(float value)
        {
            _se.Value = value;
        }
        
        public void SetBgmSeVolume(float value)
        {
            _bgm.Value = value;
        }

        public void Dispose()
        {
            _se?.Dispose();
            _bgm?.Dispose();
        }
    }
}