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
        public void Initialize(ScoreDto scoreDto)
        {
            _demoLane.Initialize(scoreDto);
            _dunceLane.Initialize(scoreDto);
        }

        // Dance or TurnChange に入った時Demoを隠す
        public void HideDemo()
        {
            _demoLane.HideAll();
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
                    _demoLane.Dance(danceData.Beat, danceData.Demo, danceData.IsSuccess);
                    _dunceLane.Dance(danceData.Beat, danceData.Dance, danceData.IsSuccess);
                    break;
                case DancePhase.Audience:
                case DancePhase.TurnChange:
                    break;
                case DancePhase.Demo:
                    _demoLane.Dance(danceData.Beat, danceData.Demo, danceData.IsSuccess);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dancePhase), dancePhase, null);
            }
        }
    }
}