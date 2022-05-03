using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.Entity.Game.NoteLine;

namespace ImitateDunce.Domain.UseCase.Game.NoteLine
{
    public sealed class LinePositionUseCase
    {
        private readonly TimeEntity _timeEntity = default;
        private readonly PositionEntity _positionEntity = default;

        public LinePositionUseCase(TimeEntity timeEntity, PositionEntity positionEntity)
        {
            _timeEntity = timeEntity;
            _positionEntity = positionEntity;
        }

        public float NormalizePosition()
        {
            return _positionEntity.NormalizePosition(0, _timeEntity.Limit, _timeEntity.Time);
        }
    }
}