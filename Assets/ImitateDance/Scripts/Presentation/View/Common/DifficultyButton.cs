using System;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class DifficultyButton : MonoBehaviour
    {
        [SerializeField] private ScreenButton _button = default;
        [SerializeField] private MusicDifficulty _difficulty = default;

        public IObservable<MusicDifficulty> OnClickAsObservable()
        {
            return _button.OnClickAsObservable().Select(_ => _difficulty);
        }
    }
}