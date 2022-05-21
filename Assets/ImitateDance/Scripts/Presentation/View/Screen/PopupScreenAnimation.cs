using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniScreen.View;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Screen
{
    public sealed class PopupScreenAnimation : ScreenAnimationBase
    {
        [SerializeField] private RectTransform _content = default;
        [SerializeField] private CanvasGroup _canvas = default;
        [SerializeField] private float _dilay = default;

        public override async UniTask Show(CancellationToken token)
        {
            if (IsShow) return;
            _content.localScale = new Vector3(1, 0, 1);
            _canvas.alpha = 0;
            if (_dilay != 0) await UniTask.Delay(TimeSpan.FromSeconds(_dilay), cancellationToken: token);
            if (token.IsCancellationRequested) return;
            await _content.DOScale(Vector3.one, 0.3f).WithCancellation(token);
            if (token.IsCancellationRequested) return;
            await _canvas.DOFade(1, 0.2f).WithCancellation(token);
            IsShow = true;
        }

        public override async UniTask Hide(CancellationToken token)
        {
            if (!IsShow) return;
            await _canvas.DOFade(0, 0.2f).WithCancellation(token);
            if (token.IsCancellationRequested) return;
            await _content.DOScale(new Vector3(1, 0, 1), 0.2f).WithCancellation(token);
            IsShow = false;
        }
    }
}