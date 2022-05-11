using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class ResultSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _selfSlider = default;
        [SerializeField] private Slider _opponentSlider = default;
        [SerializeField] private TextMeshProUGUI _selfPercent = default;
        [SerializeField] private TextMeshProUGUI _opponentPercent = default;
        [SerializeField] private TextMeshProUGUI _selfPoint = default;
        [SerializeField] private TextMeshProUGUI _opponentPoint = default;

        private void Awake()
        {
            _selfSlider.value = 0;
            _opponentSlider.value = 0;
            _selfPercent.text = "";
            _opponentPercent.text = "";
            _selfPoint.text = "";
            _opponentPoint.text = "";
        }

        public async UniTask PlayIntroAsync(int selfPoint, int opponentPoint, CancellationToken token)
        {
            var task1 = _selfSlider.DOValue(0.25f, 1).ToUniTask(cancellationToken: token);
            var task2 = _opponentSlider.DOValue(0.25f, 1).ToUniTask(cancellationToken: token);
            var percent = 0f;
            var task3 = DOTween.To(() => percent, x => percent = x, 25f, 1)
                .OnUpdate(() =>
                {
                    _selfPercent.text = $"{percent:F1}%";
                    _opponentPercent.text = $"{percent:F1}%";
                }).ToUniTask(cancellationToken: token);
            var point = 0;
            var target = (selfPoint + opponentPoint) / (2 * 4);
            var task4 = DOTween.To(() => point, x => point = x, target, 1)
                .OnUpdate(() =>
                {
                    _selfPoint.text = $"{point}pt";
                    _opponentPoint.text = $"{point}pt";
                }).ToUniTask(cancellationToken: token);

            await UniTask.WhenAll(task1, task2, task3, task4);
        }

        public void SetResult(int selfPoint, int opponentPoint)
        {
            var selfPercent = (float)selfPoint / (selfPoint + opponentPoint);
            _selfSlider.value = selfPercent;
            _opponentSlider.value = 1 - selfPercent;
            _selfPercent.text = $"{selfPercent * 100:F1}%";
            _opponentPercent.text = $"{(1 - selfPercent) * 100:F1}%";
            _selfPoint.text = $"{selfPoint}pt";
            _opponentPoint.text = $"{opponentPoint}pt";
        }
    }
}