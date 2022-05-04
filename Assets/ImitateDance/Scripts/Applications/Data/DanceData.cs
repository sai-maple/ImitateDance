using ImitateDance.Scripts.Applications.Enums;

namespace ImitateDance.Scripts.Applications.Data
{
    public sealed class DanceData
    {
        public int Beat { get; }
        public DanceDirection Demo { get; }
        public DanceDirection Dance { get; }
        public bool IsSuccess => Demo == Dance;

        public DanceData(int beat, DanceDirection dance)
        {
            Beat = beat;
            Dance = dance;
        }

        public DanceData(int beat, DanceDirection demo, DanceDirection dance)
        {
            Beat = beat;
            Demo = demo;
            Dance = dance;
        }
    }
}