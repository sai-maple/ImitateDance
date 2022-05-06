using System;
using System.Collections.Generic;
using UnityEngine;

namespace ImitateDance.Scripts.Applications.Data
{
    [Serializable]
    public sealed class ScoreDto
    {
        [SerializeField] private List<NotesDto> scores;
        [SerializeField] private int bpm;

        public List<NotesDto> Scores => scores;
        public int Bpm => bpm;

        public ScoreDto(List<NotesDto> scores, int bpm)
        {
            this.scores = scores;
            this.bpm = bpm;
        }
    }

    [Serializable]
    public sealed class NotesDto
    {
        [SerializeField] private List<NoteDto> score;
        public IReadOnlyList<NoteDto> Score => score;

        public NotesDto(List<NoteDto> score)
        {
            this.score = score;
        }
    }

    [Serializable]
    public sealed class NoteDto
    {
        [SerializeField] private int beet;
        [SerializeField] private float time;

        public int Beat => beet;
        public float Time => time;

        public NoteDto(int beet, float time)
        {
            this.beet = beet;
            this.time = time;
        }
    }
}