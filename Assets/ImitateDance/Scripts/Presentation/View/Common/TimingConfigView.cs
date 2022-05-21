using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class TimingConfigView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = default;
        [SerializeField] private Button _upButton = default;
        [SerializeField] private Button _downButton = default;
        [SerializeField] private AudioSource _audioSource = default;

        public IObservable<Unit> OnUpAsObservable()
        {
            return _upButton.OnClickAsObservable();
        }

        public IObservable<Unit> OnDownAsObservable()
        {
            return _downButton.OnClickAsObservable();
        }

        public void OnChanged(int value)
        {
            _text.text = value.ToString();
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}