using System;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class LanePairView : MonoBehaviour
    {
        [SerializeField] private LaneView _demoLane = default;
        [SerializeField] private LaneView _dunceLane = default;

        // Audienceのタイミングで初期化
        public void Initialize(NotesDto notesDto)
        {
            _demoLane.Initialize(notesDto);
        }

        public void UpdateNote(DancePhase dancePhase, NoteData noteData)
        {
            // 現在流れている譜面の次の譜面を更新する
            switch (dancePhase)
            {
                case DancePhase.Dance:
                    _demoLane.UpdateNote(noteData);
                    break;
                case DancePhase.Demo:
                    _dunceLane.UpdateNote(noteData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dancePhase), dancePhase, null);
            }
        }

        public void Play(DancePhase dancePhase, float normalizeLate)
        {
            switch (dancePhase)
            {
                case DancePhase.Dance:
                    _dunceLane.Play(normalizeLate);
                    break;
                case DancePhase.Demo:
                    _demoLane.Play(normalizeLate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dancePhase), dancePhase, null);
            }
        }

        public void OnDance(DancePhase dancePhase, DanceData danceData)
        {
            switch (dancePhase)
            {
                case DancePhase.Dance:
                    _dunceLane.Dance(danceData.Beat, danceData.Dance, danceData.IsSuccess);
                    break;
                case DancePhase.Demo:
                    _demoLane.Dance(danceData.Beat, danceData.Demo, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dancePhase), dancePhase, null);
            }
        }
    }
}