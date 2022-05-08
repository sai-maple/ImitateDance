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

        private const string HideMessage = "隠し中";
        private const string DisplayMessage = "表示中";

        public IObservable<bool> OnChangeAsObservable()
        {
            return _toggle.onValueChanged.AsObservable();
        }

        public void Initialize(bool isOn)
        {
            _toggle.isOn = isOn;
            _text.text = isOn ? HideMessage : DisplayMessage;
            _image.color = isOn ? Color.red : Color.cyan;
            _toggle.onValueChanged.AddListener(on =>
            {
                _text.text = on ? HideMessage : DisplayMessage;
                _image.color = on ? Color.red : Color.cyan;
                _audioSource.PlayOneShot(_audioSource.clip);
            });
        }
    }
}