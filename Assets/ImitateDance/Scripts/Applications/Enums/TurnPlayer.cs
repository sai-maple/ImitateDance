using System;

namespace ImitateDance.Scripts.Applications.Enums
{
    public enum TurnPlayer
    {
        Self,
        Opponent
    }

    public static class TurnPlayerExtension
    {
        public static TurnPlayer Next(this TurnPlayer self)
        {
            var o = self + 1;
            return Enum.IsDefined(typeof(TurnPlayer), o) ? o : TurnPlayer.Self;
        }

        public static TurnPlayer Reverse(this TurnPlayer self)
        {
            return self == TurnPlayer.Self ? TurnPlayer.Opponent : TurnPlayer.Self;
        }
    }
}