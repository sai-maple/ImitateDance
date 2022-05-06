using System;

namespace ImitateDance.Scripts.Applications.Enums
{
    [Flags]
    public enum DanceDirection
    {
        Non = Up | Down | Right | Left,
        Up = 1 << 0,
        Down = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3
    }
}