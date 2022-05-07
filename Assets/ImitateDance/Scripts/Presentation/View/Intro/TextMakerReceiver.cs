using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace ImitateDance.Scripts.Presentation.View.Intro
{
    public sealed class TextMakerReceiver : MonoBehaviour, INotificationReceiver
    {
        [SerializeField] private RectTransform _speech = default;
        [SerializeField] private TextMeshProUGUI _text = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        private void Awake()
        {
            _speech.localScale = Vector3.zero;
            _text.text = string.Empty;
        }

        public async void OnNotify(Playable origin, INotification notification, object context)
        {
            var element = notification as TextMakerView;
            if (element == null) return;

            await _speech.DOScale(1, 0.2f).WithCancellation(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await _text.DOText(element.Message, element.Message.Length * 0.05f).WithCancellation(_cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _cancellation.Token);
            if (_cancellation.IsCancellationRequested) return;
            _text.text = string.Empty;
            await _speech.DOScale(0, 0.2f).WithCancellation(_cancellation.Token);
        }

        private void OnDestroy()
        {
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}