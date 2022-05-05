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
    }

    [Serializable]
    public sealed class ScoreData
    {
        [SerializeField] private List<NoteData> score;
        public IReadOnlyList<NoteData> Score => score;
    }

    [Serializable]
    public sealed class NoteData
    {
        [SerializeField] private int beet;

        public int Beat => beet;
        public float Time { get; set; }
    }
}