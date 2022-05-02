namespace ImitateDunce.Applications.Enums
{
    public enum Dunce
    {
        Non = Up | Down | Right | Left,
        Up = 1 << 0,
        Down = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3
    }
}