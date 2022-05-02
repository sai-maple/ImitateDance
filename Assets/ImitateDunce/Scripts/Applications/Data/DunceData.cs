using ImitateDunce.Applications.Enums;

namespace ImitateDunce.Applications.Data
{
    public sealed class DunceData
    {
        public int Beat { get; }
        public Dunce Demo { get; }
        public Dunce Dunce { get; }

        public DunceData(int beat, Dunce dunce)
        {
            Beat = beat;
            Dunce = dunce;
        }

        public DunceData(int beat, Dunce demo, Dunce dunce)
        {
            Beat = beat;
            Demo = demo;
            Dunce = dunce;
        }
    }
}