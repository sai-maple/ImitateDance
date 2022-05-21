using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ImitateDance.Scripts.Presentation.View.Common
{
    public sealed class TimingLineView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _content = default;
        [SerializeField] private RectTransform _currentLine = default;
        [SerializeField] private RectTransform _tapLineRect = default;
        [SerializeField] private CanvasGroup _tapLineImage = default;
        [SerializeField] private AudioSource _audioSource = default;

        private readonly Subject<Unit> _subject = new Subject<Unit>();
        private float _size = default;
        private Vector2 _position = default;

        public IObservable<Unit> OnFireAsObservable()
        {
            return _subject.Share();
        }

        public void Initialize(IEnumerable<KeyCode> keyCodes)
        {
            _size = _content.sizeDelta.x / 2;
            Observable.EveryUpdate()
                .TakeUntilDestroy(this)
                .Where(_ => Input.anyKeyDown)
                .Subscribe(_ => Fire(keyCodes));
        }

        private void Fire(IEnumerable<KeyCode> keyCodes)
        {
            if (!keyCodes.Where(Input.GetKeyDown).Any()) return;
            _subject.OnNext(Unit.Default);
        }

        public void OnFire(float normalizePos)
        {
            _position.x = normalizePos * _size;
            _tapLineRect.anchoredPosition = _position;
            _tapLineImage.alpha = 1;
            _tapLineImage.DOFade(0, 0.5f);
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        public void OnUpdateLine(float normalizePos)
        {
            _position.x = normalizePos * _size;
            _currentLine.anchoredPosition = _position;
        }

        private void OnDestroy()
        {
            _subject.OnCompleted();
            _subject.Dispose();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _subject.OnNext(Unit.Default);
        }
    }
}