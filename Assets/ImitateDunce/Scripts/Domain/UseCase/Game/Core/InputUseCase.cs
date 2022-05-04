using System;
using ImitateDunce.Applications.Common;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;

namespace ImitateDunce.Domain.UseCase.Game.Core
{
    public sealed class InputUseCase
    {
        private readonly ScoreEntity _scoreEntity = default;
        private readonly TimeEntity _timeEntity = default;
        private readonly PhaseEntity _phaseEntity = default;
        private readonly PointEntity _pointEntity = default;
        private readonly TurnPlayerEntity _turnPlayerEntity = default;

        public InputUseCase(ScoreEntity scoreEntity, TimeEntity timeEntity, PhaseEntity phaseEntity,
            PointEntity pointEntity, TurnPlayerEntity turnPlayerEntity)
        {
            _scoreEntity = scoreEntity;
            _timeEntity = timeEntity;
            _phaseEntity = phaseEntity;
            _pointEntity = pointEntity;
            _turnPlayerEntity = turnPlayerEntity;
        }

        public void OnTap(DunceDirection direction)
        {
            Logger.Log(direction);
            switch (_phaseEntity.Current)
            {
                case DuncePhase.Dunce:
                    var point = _scoreEntity.OnDunce(_timeEntity.Time, direction);
                    _pointEntity.Add(_turnPlayerEntity.Current, point);
                    break;
                case DuncePhase.Demo:
                    _scoreEntity.OnDemo(_timeEntity.Time, direction);
                    break;
                case DuncePhase.Audience:
                case DuncePhase.TurnChange:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}