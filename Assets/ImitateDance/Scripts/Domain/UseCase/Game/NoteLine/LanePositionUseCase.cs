using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.Entity.Game.NoteLine;

namespace ImitateDance.Scripts.Domain.UseCase.Game.NoteLine
{
    public sealed class LanePositionUseCase
    {
        private readonly TimeEntity _timeEntity = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly PositionEntity _positionEntity = default;

        public LanePositionUseCase(TimeEntity timeEntity, MusicEntity musicEntity, PositionEntity positionEntity)
        {
            _timeEntity = timeEntity;
            _musicEntity = musicEntity;
            _positionEntity = positionEntity;
        }

        public float NormalizePosition()
        {
            return _positionEntity.NormalizePosition(0, _musicEntity.DanceTime, _timeEntity.Time);
        }
    }
}