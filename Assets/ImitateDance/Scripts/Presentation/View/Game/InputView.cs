using System;
using DG.Tweening;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class InputView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image = default;
        [SerializeField] private Animator _animator = default;
        [SerializeField] private DanceDirection _direction = default;

        private readonly Subject<DanceDirection> _subject = new Subject<DanceDirection>();
        private static readonly int TapHash = Animator.StringToHash("Tap");

        public IObservable<DanceDirection> OnTapAsObservable()
        {
            return _subject.Share();
        }

        public void Initialize(KeyCode keyCode, bool isHide)
        {
            Observable.EveryUpdate()
                .TakeUntilDestroy(this)
                .Where(_ => Input.GetKeyDown(keyCode))
                .Subscribe(_ => Fire());
            _image.gameObject.SetActive(!isHide);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Fire();
        }

        private void Fire()
        {
            _animator.SetTrigger(TapHash);
            _subject.OnNext(_direction);
        }

        private void OnDestroy()
        {
            _subject.OnCompleted();
            _subject.Dispose();
        }
    }
}