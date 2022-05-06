using System;
using System.Collections.Generic;
using UnityEngine;

namespace ImitateDance.Scripts.Applications.Data
{
    [Serializable]
    public sealed class ScoreDto
    {
        [SerializeField] private List<ScoreData> scores;
        [SerializeField] private int bpm;

        public List<ScoreData> Scores => scores;
        public int Bpm => bpm;

        public ScoreDto(List<ScoreData> scores, int bpm)
        {
            this.scores = scores;
            this.bpm = bpm;
        }
    }

    [Serializable]
    public sealed class ScoreData
    {
        [SerializeField] private List<NoteData> score;
        public IReadOnlyList<NoteData> Score => score;

        public ScoreData(List<NoteData> score)
        {
            this.score = score;
        }
    }

    [Serializable]
    public sealed class NoteData
    {
        [SerializeField] private int beet;
        [SerializeField] private float time;

        public int Beat => beet;
        public float Time => time;

        public NoteData(int beet, float time)
        {
            this.beet = beet;
            this.time = time;
        }
    }
}