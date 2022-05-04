using System;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class TurnPlayerEntity : IDisposable
    {
        public TurnPlayer Current { get; private set; }
        private readonly Subject<TurnPlayer> _subject = default;

        public TurnPlayerEntity()
        {
            _subject = new Subject<TurnPlayer>();
        }

        public void GameStart()
        {
            Current = TurnPlayer.Opponent;
            _subject.OnNext(Current);
        }

        public void NextTurn()
        {
            Current = Current.Next();
            _subject.OnNext(Current);
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}