using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDunce.Presentation.View.Common
{
    [RequireComponent(typeof(Button), typeof(AudioSource))]
    public sealed class ScreenButton : MonoBehaviour
    {
        [SerializeField] private string _screenName = default;
        private Button _button = default;

        private void Awake()
        {
            if (!TryGetComponent(out _button)) return;
            if (!TryGetComponent<AudioSource>(out var audioSource)) return;

            if (audioSource.clip == null) return;
            _button.onClick.AddListener(() => audioSource.PlayOneShot(audioSource.clip));
        }

        public IObservable<string> OnClickAsObservable()
        {
            _button = GetComponent<Button>();
            return _button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(500))
                .Select(_ => _screenName);
        }
    }
}