using System;

namespace ImitateDunce.Applications.Enums
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
    }
}