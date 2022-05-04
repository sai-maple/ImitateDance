using System;
using ImitateDance.Scripts.Applications.Enums;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class PointEntity
    {
        private int _selfPoint = default;
        private int _opponentPoint = default;

        public PointEntity()
        {
            _selfPoint = 0;
            _opponentPoint = 0;
        }

        public void Add(TurnPlayer player, int point)
        {
            switch (player)
            {
                case TurnPlayer.Self:
                    _selfPoint += point;
                    break;
                case TurnPlayer.Opponent:
                    _opponentPoint += point;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public void Bonus(TurnPlayer player, bool isPerfect)
        {
            // todo ボーナスの計算
            if (!isPerfect) return;
            switch (player)
            {
                case TurnPlayer.Self:
                    _selfPoint += 100;
                    break;
                case TurnPlayer.Opponent:
                    _opponentPoint += 100;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }

        public TurnPlayer Winner()
        {
            return _selfPoint >= _opponentPoint ? TurnPlayer.Self : TurnPlayer.Opponent;
        }
    }
}