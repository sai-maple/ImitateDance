using System;
using DG.Tweening;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class InputView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _image = default;
        [SerializeField] private DanceDirection _direction = default;
        [SerializeField] private KeyCode _keyCode = default;

        private readonly Subject<DanceDirection> _subject = new Subject<DanceDirection>();

        public IObservable<DanceDirection> OnTapAsObservable()
        {
            return _subject.Share();
        }

        private void Awake()
        {
            Observable.EveryUpdate()
                .TakeUntilDestroy(this)
                .Where(_ => Input.GetKeyDown(_keyCode))
                .Subscribe(_ => Fire());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Fire();
        }

        private void Fire()
        {
            _image.DOShakeScale(0.3f, 1.1f);
            _subject.OnNext(_direction);
        }

        private void OnDestroy()
        {
            _subject.OnCompleted();
            _subject.Dispose();
        }
    }
}