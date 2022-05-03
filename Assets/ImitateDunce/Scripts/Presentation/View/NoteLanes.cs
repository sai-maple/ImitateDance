using System;
using ImitateDunce.Applications.Data;
using ImitateDunce.Applications.Enums;
using UnityEngine;

namespace ImitateDunce.Presentation.View
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

        // Dunce or TurnChange に入った時Demoを隠す
        public void HideDemo()
        {
            _demoLane.HideAll();
        }
        
        public void Play(DuncePhase duncePhase, float normalizeLate)
        {
            switch (duncePhase)
            {
                case DuncePhase.Dunce:
                    _dunceLane.Play(normalizeLate);
                    break;
                case DuncePhase.Audience:
                case DuncePhase.TurnChange:
                    break;
                case DuncePhase.Demo:
                    _demoLane.Play(normalizeLate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(duncePhase), duncePhase, null);
            }
        }

        public void OnDunce(DuncePhase duncePhase, DunceData dunceData)
        {
            switch (duncePhase)
            {
                case DuncePhase.Dunce:
                    _demoLane.Dunce(dunceData.Beat, dunceData.Demo, dunceData.IsSuccess);
                    _dunceLane.Dunce(dunceData.Beat, dunceData.Dunce, dunceData.IsSuccess);
                    break;
                case DuncePhase.Audience:
                case DuncePhase.TurnChange:
                    break;
                case DuncePhase.Demo:
                    _demoLane.Dunce(dunceData.Beat, dunceData.Demo, dunceData.IsSuccess);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(duncePhase), duncePhase, null);
            }
        }
    }
}