using System;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class VolumeEntity : IDisposable
    {
        private readonly ReactiveProperty<float> _se = default;
        private readonly ReactiveProperty<float> _bgm = default;

        public float SeVolume => _se.Value;
        public float BgmVolume => _bgm.Value;

        public VolumeEntity()
        {
            _se = new ReactiveProperty<float>(0.5f);
            _bgm = new ReactiveProperty<float>(0.5f);
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
        
        public void SetBgmVolume(float value)
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