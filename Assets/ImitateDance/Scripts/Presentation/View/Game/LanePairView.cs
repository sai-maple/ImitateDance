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
        public void Initialize(ScoreData scoreData)
        {
            Debug.Log(scoreData.Score);
            _demoLane.Initialize(scoreData);
            _dunceLane.Initialize(scoreData);
        }

        public void Play(DancePhase dancePhase, float normalizeLate)
        {
            switch (dancePhase)
            {
                case DancePhase.Dance:
                    _dunceLane.Play(normalizeLate);
                    break;
                case DancePhase.Audience:
                case DancePhase.TurnChange:
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
                case DancePhase.Audience:
                    _demoLane.Dance(danceData.Beat, danceData.Demo, true);
                    _dunceLane.Dance(danceData.Beat, danceData.Dance, danceData.IsSuccess);
                    break;
                case DancePhase.Demo:
                case DancePhase.TurnChange:
                    _demoLane.Dance(danceData.Beat, danceData.Demo, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dancePhase), dancePhase, null);
            }
        }
    }
}