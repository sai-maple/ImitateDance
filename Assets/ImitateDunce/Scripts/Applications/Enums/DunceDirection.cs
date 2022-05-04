namespace ImitateDunce.Applications.Enums
{
    public enum DunceDirection
    {
        Non = Up | Down | Right | Left,
        Up = 1 << 1,
        Down = 1 << 2,
        Right = 1 << 3,
        Left = 1 << 4
    }
}