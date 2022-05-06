using ImitateDance.Scripts.Applications.Enums;

namespace ImitateDance.Scripts.Applications.Data
{
    public sealed class NoteData
    {
        public int Beat { get; }
        public bool HasDirection { get; }
        public DanceDirection Direction { get; }

        public NoteData(int beat, bool hasDirection)
        {
            Beat = beat;
            HasDirection = hasDirection;
            Direction = DanceDirection.Non;
        }

        public NoteData(int beat, bool hasDirection, DanceDirection direction)
        {
            Beat = beat;
            HasDirection = hasDirection;
            Direction = direction;
        }
    }
}