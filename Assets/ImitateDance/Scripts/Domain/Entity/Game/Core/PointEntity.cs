using System;
using ImitateDance.Scripts.Applications.Common;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class PointEntity : IDisposable
    {
        private readonly ReactiveProperty<int> _selfPoint = default;
        private readonly ReactiveProperty<int> _opponentPoint = default;
        private readonly Subject<TurnPlayer> _subject = default;

        public PointEntity()
        {
            _selfPoint = new ReactiveProperty<int>(0);
            _opponentPoint = new ReactiveProperty<int>(0);
            _subject = new Subject<TurnPlayer>();
        }

        public IObservable<int> OnSelfPointChangeAsObservable()
        {
            return _selfPoint;
        }

        public IObservable<int> OnOpponentPointChangeAsObservable()
        {
            return _opponentPoint;
        }

        public IObservable<TurnPlayer> OnBonusAsObservable()
        {
            return _subject.Share();
        }

        public void Add(TurnPlayer player, int point)
        {
            if (point == 0) return;
            switch (player)
            {
                case TurnPlayer.Self:
                    _selfPoint.Value += point;
                    break;
                case TurnPlayer.Opponent:
                    _opponentPoint.Value += point;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }

            _subject.OnNext(player);
            Logger.Log($"self : {_selfPoint} , opponent :d {_opponentPoint}  {player}+{point}");
        }

        public void Bonus(TurnPlayer player, bool isPerfect)
        {
            if (!isPerfect) return;
            switch (player)
            {
                case TurnPlayer.Self:
                    _selfPoint.Value += 10;
                    break;
                case TurnPlayer.Opponent:
                    _opponentPoint.Value += 10;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public TurnPlayer Winner()
        {
            return _selfPoint.Value >= _opponentPoint.Value ? TurnPlayer.Self : TurnPlayer.Opponent;
        }

        public void Dispose()
        {
            _selfPoint.Dispose();
            _opponentPoint.Dispose();
            _subject?.Dispose();
        }
    }
}