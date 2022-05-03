using ImitateDunce.Applications.Enums;

namespace ImitateDunce.Applications.Data
{
    public sealed class DunceData
    {
        public int Beat { get; }
        public DunceDirection Demo { get; }
        public DunceDirection Dunce { get; }
        public bool IsSuccess => Demo == Dunce;

        public DunceData(int beat, DunceDirection dunce)
        {
            Beat = beat;
            Dunce = dunce;
        }

        public DunceData(int beat, DunceDirection demo, DunceDirection dunce)
        {
            Beat = beat;
            Demo = demo;
            Dunce = dunce;
        }
    }
}