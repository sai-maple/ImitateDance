using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class LitMaterialView : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.white;
        private Material _material = default;
        private static readonly int ColorID = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            if (!TryGetComponent<Image>(out var image)) return;
            _material = image.material;

            Observable.EveryUpdate()
                .TakeUntilDestroy(this)
                .Subscribe(_ => _material.SetColor(ColorID, _color));
        }
    }
}