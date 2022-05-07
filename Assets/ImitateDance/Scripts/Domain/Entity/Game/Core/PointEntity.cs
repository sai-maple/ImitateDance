using System;
using ImitateDance.Scripts.Applications.Common;
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
            if (point == 0) return;
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

            Logger.Log($"self : {_selfPoint} , opponent :d {_opponentPoint}  {player}+{point}");
        }

        public void Bonus(TurnPlayer player, bool isPerfect)
        {
            // todo ボーナスの計算
            if (!isPerfect) return;
            switch (player)
            {
                case TurnPlayer.Self:
                    _selfPoint += 10;
                    break;
                case TurnPlayer.Opponent:
                    _opponentPoint += 10;
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