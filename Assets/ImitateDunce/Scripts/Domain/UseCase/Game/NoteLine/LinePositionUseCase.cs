using ImitateDunce.Domain.Entity.Game.Core;
using ImitateDunce.Domain.Entity.Game.NoteLine;

namespace ImitateDunce.Domain.UseCase.Game.NoteLine
{
    public sealed class PositionUseCase
    {
        private readonly TimeEntity _timeEntity = default;
        private readonly PositionEntity _positionEntity = default;

        public PositionUseCase(TimeEntity timeEntity, PositionEntity positionEntity)
        {
            _timeEntity = timeEntity;
            _positionEntity = positionEntity;
        }

        public float NormalizePosition()
        {
            
        }
    }
}