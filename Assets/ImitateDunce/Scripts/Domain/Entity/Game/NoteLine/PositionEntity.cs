using UnityEngine;

namespace ImitateDunce.Domain.Entity.Game.NoteLine
{
    public sealed class PositionEntity
    {
        public float NormalizePosition(float from, float to, float current)
        {
            var t = (current - from) / (to - from);
            return Mathf.Clamp(t, 0, 1);
        }
    }
}