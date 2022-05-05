using System;
using ImitateDance.Scripts.Applications.Enums;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class DifficultyView : MonoBehaviour
    {
        [SerializeField] private Button _previousButton = default;
        [SerializeField] private Button _nextButton = default;
        [SerializeField] private TextMeshProUGUI _text = default;

        private readonly string[] _message = new[] { "かんたーん", "ちょいヤバ", "おにヤバ" };

        private void Awake()
        {
            if (!TryGetComponent<AudioSource>(out var audioSource)) return;
            if (audioSource.clip == null) return;
            _previousButton.onClick.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
            _nextButton.onClick.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
        }

        public IObservable<Unit> OnNextAsObservable()
        {
            return _nextButton.OnClickAsObservable();
        }

        public IObservable<Unit> OnPreviousAsObservable()
        {
            return _previousButton.OnClickAsObservable();
        }

        public void SetDifficult(MusicDifficulty difficulty)
        {
            _text.text = _message[(int)difficulty];
        }
    }
}