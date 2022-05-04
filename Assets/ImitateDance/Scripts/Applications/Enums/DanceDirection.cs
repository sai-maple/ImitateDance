namespace ImitateDance.Scripts.Applications.Enums
{
    public enum DanceDirection
    {
        Non = Up | Down | Right | Left,
        Up = 1 << 1,
        Down = 1 << 2,
        Right = 1 << 3,
        Left = 1 << 4
    }
}