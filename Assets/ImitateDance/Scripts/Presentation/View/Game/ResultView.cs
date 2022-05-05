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

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class ResultView : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _winDirector = default;
        [SerializeField] private PlayableDirector _loseDirector = default;
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

        public async UniTask PlayAsync(TurnPlayer winner, CancellationToken token)
        {
            var resultTask = winner == TurnPlayer.Self ? _winDirector.PlayAsync(token) : _loseDirector.PlayAsync(token);
            await resultTask;
            _returnButtonCanvas.DOFade(1, 0.5f);
        }
    }
}