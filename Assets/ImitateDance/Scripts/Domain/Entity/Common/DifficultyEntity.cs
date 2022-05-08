using System;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class DifficultyEntity : IDisposable
    {
        private readonly ReactiveProperty<MusicDifficulty> _property = default;
        public MusicDifficulty Value => _property.Value;

        public DifficultyEntity()
        {
            _property = new ReactiveProperty<MusicDifficulty>(MusicDifficulty.Easy);
        }

        public IObservable<MusicDifficulty> OnChangeAsObservable()
        {
            return _property;
        }

        public void Set(MusicDifficulty difficulty)
        {
            _property.Value = difficulty;
        }

        public void Dispose()
        {
            _property.Dispose();
        }
    }
}