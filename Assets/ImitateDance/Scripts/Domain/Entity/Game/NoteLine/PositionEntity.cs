using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Game.NoteLine
{
    public sealed class PositionEntity
    {
        public float NormalizePosition(float from, float to, double current)
        {
            var t = (current - from) / (to - from);
            return Mathf.Clamp((float)t, 0, 1);
        }
    }
}