using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using UnityEditor;
using UnityEngine;

namespace ImitateDance.Editor
{
    public sealed class ScoreConvertEditor : EditorWindow
    {
        [SerializeField] private TextAsset _score = default;
        [SerializeField] private MusicDifficulty _difficulty = default;

        [MenuItem("Tools/ScoreConvert")]
        private static void ShowWindow()
        {
            GetWindow<ScoreConvertEditor>("ViewTestWindow");
        }

        private void OnGUI()
        {
            GUILayout.Label("");
            GUILayout.Label("------------------------------");
            GUILayout.Label("NoteEditorのjsonをこのゲームのフォーマットに変換します");

            var so = new SerializedObject(this);
            so.Update();
            EditorGUILayout.PropertyField(so.FindProperty("_score"), true);
            EditorGUILayout.PropertyField(so.FindProperty("_difficulty"), true);
            so.ApplyModifiedProperties();

            if (!GUILayout.Button("変換")) return;
            var notes = JsonUtility.FromJson<Notes>(_score.text);
            var scoreDto = Convert(notes);

            var filePath = $"Assets/ImitateDance/Score/{_difficulty}.json";
            var outJson = JsonUtility.ToJson(scoreDto);
            File.WriteAllText(filePath, outJson);
        }

        private static ScoreDto Convert(Notes notes)
        {
            var beetTime = 60f / notes.BPM;
            // LPBは2固定
            var numTime = beetTime / 2f;
            var score = notes.notes
                .GroupBy(x => x.num / 16)
                .Where(group => group.Key % 2 == 0)
                .Select(group =>
                    new ScoreData(group.Select(note => new NoteData(note.num + 1, (note.num + 1) * numTime)).ToList()))
                .ToList();
            return new ScoreDto(score, notes.BPM);
        }

        [Serializable]
        private sealed class Notes
        {
            public string name;
            public int maxBlock;
            public int BPM;
            public int offset;
            public List<Note> notes = new List<Note>();
        }

        [Serializable]
        private sealed class Note
        {
            public int LPB;
            public int num;
            public int block;
            public int type;
            public List<Note> notes = new List<Note>();
        }
    }
}