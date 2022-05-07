using System;
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
            var point = 0;
            switch (_phaseEntity.Current)
            {
                case DancePhase.Dance:
                    point = _scoreEntity.OnDance(_timeEntity.Time, direction);
                    break;
                case DancePhase.Demo:
                    point = _scoreEntity.OnDemo(_timeEntity.Time, direction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _pointEntity.Add(_turnPlayerEntity.Current, point);
        }

        public void CpuInput()
        {
            if (_turnPlayerEntity.Current == TurnPlayer.Self) return;
            var point = 0;
            switch (_phaseEntity.Current)
            {
                case DancePhase.Dance:
                    point = _scoreEntity.CpuDance(_timeEntity.Time);
                    break;
                case DancePhase.Demo:
                    point = _scoreEntity.CPUDemo(_timeEntity.Time, DanceDirectionExtension.RandomOne());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _pointEntity.Add(_turnPlayerEntity.Current, point);
        }
    }
}