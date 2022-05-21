using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class HideButtonToggle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = default;
        [SerializeField] private Image _image = default;
        [SerializeField] private Toggle _toggle = default;
        [SerializeField] private AudioSource _audioSource = default;

        private const string HideMessage = "かくしちゅう";
        private const string DisplayMessage = "ひょうじちゅう";

        public IObservable<bool> OnChangeAsObservable()
        {
            return _toggle.onValueChanged.AsObservable();
        }

        public void Initialize(bool isOn)
        {
            _toggle.isOn = isOn;
            _text.text = isOn ? HideMessage : DisplayMessage;
            _image.color = isOn ? new Color(1f, 0.56f, 0.45f) : new Color(0.58f, 1f, 0.89f);
            _toggle.onValueChanged.AddListener(on =>
            {
                _text.text = on ? HideMessage : DisplayMessage;
                _image.color = on ? new Color(1f, 0.56f, 0.45f) : new Color(0.58f, 1f, 0.89f);
                _audioSource.PlayOneShot(_audioSource.clip);
            });
        }
    }
}