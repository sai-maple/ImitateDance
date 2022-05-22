using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Tutorial
{
    public sealed class TutorialView : MonoBehaviour
    {
        [SerializeField] private List<SpeechList> _speeches = default;
        [SerializeField] private Button _nextButton = default;
        [SerializeField] private Image _nextImage = default;
        [SerializeField] private RectTransform _speech = default;
        [SerializeField] private TextMeshProUGUI _text = default;
        [SerializeField] private AudioSource _audioSource = default;
        [SerializeField] private AudioClip _audioClip = default;
        [SerializeField] private float _delay = default;

        private void Awake()
        {
            _speech.localScale = Vector3.zero;
            _text.text = "";
            _nextImage.enabled = false;
        }

        public async UniTask PlayAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: token);
            foreach (var speech in _speeches)
            {
                _text.text = "";
                _nextImage.enabled = false;
                await _speech.DOScale(1, 0.3f).WithCancellation(token);
                if (token.IsCancellationRequested) return;

                foreach (var speechData in speech.Speeches)
                {
                    await DoText(speechData, token);
                    if (token.IsCancellationRequested) return;
                }

                await _speech.DOScale(0, 0.2f).WithCancellation(token);
                if (token.IsCancellationRequested) return;
            }
        }

        private async UniTask DoText(SpeechData speech, CancellationToken token)
        {
            // メッセージ送り開始
            _nextImage.enabled = false;
            _text.text = "";
            for (var i = 0; i < speech.Animator.Length; i++)
            {
                speech.Animator[i].SetTrigger(speech.AnimationTrigger[i]);
            }

            var duration = speech.Speech.Length * 0.05f;
            var tween = _text.DOText(speech.Speech, duration);
            var tweenTask = UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
            var skipEvent = _nextButton.onClick.GetAsyncEventHandler(CancellationToken.None);
            _audioSource.Play();
            // ボタン待ち or テキストの再生完了待ち
            await UniTask.WhenAny(skipEvent.OnInvokeAsync(), tweenTask);
            if (token.IsCancellationRequested) return;
            _audioSource.Stop();

            tween.Kill();
            _text.text = speech.Speech;
            _nextImage.enabled = true;
            var nextEvent = _nextButton.onClick.GetAsyncEventHandler(CancellationToken.None);
            await nextEvent.OnInvokeAsync();
            if (token.IsCancellationRequested) return;
            _audioSource.PlayOneShot(_audioClip);
            _nextImage.enabled = true;
        }
    }

    [Serializable]
    public sealed class SpeechList
    {
        public List<SpeechData> Speeches;
    }

    [Serializable]
    public sealed class SpeechData
    {
        public string Speech;
        public Animator[] Animator;
        public string[] AnimationTrigger;
    }
}