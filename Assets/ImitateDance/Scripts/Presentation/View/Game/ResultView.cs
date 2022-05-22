using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using UniScreen.Extension;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class ResultView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _winDirector = default;
        [SerializeField] private PlayableDirector _loseDirector = default;
        [SerializeField] private PlayableDirector _resultDirector = default;
        [SerializeField] private ResultSliderView _resultSliderView = default;
        [SerializeField] private CloseScreenButton _returnButton = default;
        [SerializeField] private CanvasGroup _returnButtonCanvas = default;

        private void Awake()
        {
            _returnButtonCanvas.alpha = 0;
        }

        public IObservable<Unit> OnCloseAsObservable()
        {
            return _returnButton.OnClickAsObservable();
        }

        public async UniTask PlaySlider(int selfPoint, int opponentPoint, CancellationToken token)
        {
            var tas1 = _resultDirector.PlayAsync(token);
            var task2 = _resultSliderView.PlayIntroAsync(selfPoint, opponentPoint, token);

            await UniTask.WhenAll(tas1, task2);
        }

        public async UniTask PlayAsync(int selfPoint, int opponentPoint, TurnPlayer winner, CancellationToken token)
        {
            _resultSliderView.SetResult(selfPoint, opponentPoint, token);
            var resultTask = winner == TurnPlayer.Self ? _winDirector.PlayAsync(token) : _loseDirector.PlayAsync(token);
            await resultTask;
            _returnButtonCanvas.DOFade(1, 0.5f).WithCancellation(token).Forget();
        }
    }
}