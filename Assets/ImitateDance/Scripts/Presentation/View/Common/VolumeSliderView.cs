using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class VolumeSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _seSlider = default;
        [SerializeField] private Slider _bgmSlider = default;
        private AudioSource _audioSource = default;

        private void Awake()
        {
            if (!TryGetComponent(out _audioSource)) return;
            if (_audioSource.clip == null) return;
            _seSlider.onValueChanged.AddListener(value =>
            {
                if (_audioSource.isPlaying) return;
                _audioSource.PlayOneShot(_audioSource.clip);
                _audioSource.volume = value;
            });
        }

        public void Initialize(float se, float bgm)
        {
            _seSlider.value = se;
            _bgmSlider.value = bgm;
        }

        public IObservable<float> OnBgmEditAsObservable()
        {
            return _bgmSlider.OnValueChangedAsObservable();
        }

        public IObservable<float> OnSeEditEndAsObservable()
        {
            return _seSlider.OnPointerUpAsObservable().Select(_ => _seSlider.value);
        }
    }
}