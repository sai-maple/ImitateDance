using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class KeyConfigView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Toggle _selectImage = default;
        [SerializeField] private TextMeshProUGUI _keyText = default;
        [SerializeField] private AudioSource _audioSource = default;

        private readonly Subject<KeyCode> _subject = new Subject<KeyCode>();

        private readonly Dictionary<KeyCode, string> _dictionary = new Dictionary<KeyCode, string>()
        {
            { KeyCode.A, "A" },
            { KeyCode.B, "B" },
            { KeyCode.C, "C" },
            { KeyCode.D, "D" },
            { KeyCode.E, "E" },
            { KeyCode.F, "F" },
            { KeyCode.G, "G" },
            { KeyCode.H, "H" },
            { KeyCode.I, "I" },
            { KeyCode.J, "J" },
            { KeyCode.K, "K" },
            { KeyCode.L, "L" },
            { KeyCode.M, "M" },
            { KeyCode.N, "N" },
            { KeyCode.O, "O" },
            { KeyCode.P, "P" },
            { KeyCode.Q, "Q" },
            { KeyCode.R, "R" },
            { KeyCode.S, "S" },
            { KeyCode.T, "T" },
            { KeyCode.U, "U" },
            { KeyCode.V, "V" },
            { KeyCode.W, "W" },
            { KeyCode.X, "X" },
            { KeyCode.Y, "Y" },
            { KeyCode.Z, "Z" },
            { KeyCode.UpArrow, "↑" },
            { KeyCode.DownArrow, "↓" },
            { KeyCode.RightArrow, "→" },
            { KeyCode.LeftArrow, "←" },
        };

        public IObservable<KeyCode> OnChangeAsObservable()
        {
            return _subject.Share();
        }

        public void Initialize(KeyCode keyCode)
        {
            if (!_dictionary.ContainsKey(keyCode)) return;
            _keyText.text = _dictionary[keyCode];
        }

        private void Update()
        {
            if (!_selectImage.isOn) return;
            if (!Input.anyKeyDown) return;
            _selectImage.isOn = false;
            foreach (var key in _dictionary.Keys.Where(Input.GetKeyDown))
            {
                _subject.OnNext(key);
                return;
            }
        }

        public void OnChanged(KeyCode keyCode)
        {
            if (!_dictionary.ContainsKey(keyCode)) return;
            _keyText.text = _dictionary[keyCode];
            if (_audioSource.clip == null) return;
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _selectImage.isOn = true;
        }

        private void OnDestroy()
        {
            _subject.OnCompleted();
            _subject.Dispose();
        }
    }
}