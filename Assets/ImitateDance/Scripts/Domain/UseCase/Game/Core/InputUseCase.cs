using System;
using ImitateDance.Scripts.Applications.Common;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;

namespace ImitateDance.Scripts.Domain.UseCase.Game.Core
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

        public void OnTap(DanceDirection direction)
        {
            Logger.Log(direction);
            switch (_phaseEntity.Current)
            {
                case DancePhase.Dance:
                    var point = _scoreEntity.OnDance(_timeEntity.Time, direction);
                    _pointEntity.Add(_turnPlayerEntity.Current, point);
                    break;
                case DancePhase.Demo:
                    _scoreEntity.OnDemo(_timeEntity.Time, direction);
                    break;
                case DancePhase.Audience:
                case DancePhase.TurnChange:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}