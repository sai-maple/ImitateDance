using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class CanvasHideAwake : MonoBehaviour
    {
        private void Awake()
        {
            if (!TryGetComponent<CanvasGroup>(out var canvasGroup)) return;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}