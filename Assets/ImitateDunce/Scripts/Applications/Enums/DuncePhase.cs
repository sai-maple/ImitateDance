using System;

namespace ImitateDunce.Applications.Enums
{
    public enum DuncePhase
    {
        EnemyDunce,
        EnemyDemo,
        EnemyToPlayer,
        PlayerDunce,
        PlayerDemo,
    }

    public static class DuncePhaseExtension
    {
        public static DuncePhase Next(this DuncePhase self)
        {
            var o = self + 1;
            return Enum.IsDefined(typeof(DuncePhase), o) ? o : DuncePhase.EnemyDunce;
        }
    }
}