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

        public void Next()
        {
            _property.Value = _property.Value.Next();
        }

        public void Previous()
        {
            _property.Value = _property.Value.Previous();
        }

        public void Dispose()
        {
            _property.Dispose();
        }
    }
}