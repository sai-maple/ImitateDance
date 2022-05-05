using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class LoadingView : MonoBehaviour
    {
        private void Awake()
        {
            if (!TryGetComponent<TextMeshProUGUI>(out var text)) return;

            var  tmpAnimator = new DOTweenTMPAnimator(text);

            const float duration = 0.3f;
            const float delay = 0.1f;
            for (var i = 0; i < tmpAnimator.textInfo.characterCount * 3; ++i)
            {
                var index = i % tmpAnimator.textInfo.characterCount;
                tmpAnimator.DOOffsetChar(index, Vector3.up * 20.0f, duration).SetEase(Ease.OutCubic).SetDelay(delay * i);
                tmpAnimator.DOOffsetChar(index, Vector3.zero, duration).SetEase(Ease.InCubic).SetDelay(delay * i + duration);
            }
        }
    }
}