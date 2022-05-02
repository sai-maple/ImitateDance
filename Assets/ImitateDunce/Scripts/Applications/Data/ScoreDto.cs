using System;
using System.Collections.Generic;

namespace ImitateDunce.Applications.Data
{
    [Serializable]
    public sealed class ScoreDto
    {
        public IReadOnlyList<NoteDto> Score { get; }

        public ScoreDto(IReadOnlyList<NoteDto> score)
        {
            Score = score;
        }
    }

    [Serializable]
    public sealed class NoteDto
    {
        public float Time { get; }
        public int Beat { get; }

        public NoteDto(float time, int beat)
        {
            Time = time;
            Beat = beat;
        }
    }
}