using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    [RequireComponent(typeof(Button), typeof(AudioSource))]
    public sealed class CloseScreenButton : MonoBehaviour
    {
        private Button _button = default;

        private void Awake()
        {
            if (!TryGetComponent(out _button)) return;
            if (!TryGetComponent<AudioSource>(out var audioSource)) return;

            if (audioSource.clip == null) return;
            _button.onClick.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
        }

        public IObservable<Unit> OnClickAsObservable()
        {
            _button = GetComponent<Button>();
            return _button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(500));
        }
    }
}