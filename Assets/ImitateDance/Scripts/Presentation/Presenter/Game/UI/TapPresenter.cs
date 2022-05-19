using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using ImitateDance.Scripts.Presentation.View.Game;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class TapPresenter : IInitializable, IDisposable
    {
        private readonly TurnPlayerEntity _turnPlayerEntity = default;
        private readonly HideButtonEntity _hideButtonEntity = default;
        private readonly KeyConfigEntity _keyConfigEntity = default;
        private readonly InputUseCase _inputUseCase = default;
        private readonly InputView _inputView = default;
        private readonly DanceDirection _direction = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public TapPresenter(TurnPlayerEntity turnPlayerEntity, HideButtonEntity hideButtonEntity,
            KeyConfigEntity keyConfigEntity, InputUseCase inputUseCase, InputView inputView, DanceDirection direction)
        {
            _turnPlayerEntity = turnPlayerEntity;
            _hideButtonEntity = hideButtonEntity;
            _keyConfigEntity = keyConfigEntity;
            _inputUseCase = inputUseCase;
            _inputView = inputView;
            _direction = direction;
        }

        public void Initialize()
        {
            var key = _direction switch
            {
                DanceDirection.Non => _keyConfigEntity.Up,
                DanceDirection.Up => _keyConfigEntity.Up,
                DanceDirection.Down => _keyConfigEntity.Down,
                DanceDirection.Right => _keyConfigEntity.Right,
                DanceDirection.Left => _keyConfigEntity.Left,
                _ => throw new ArgumentOutOfRangeException()
            };
            _inputView.Initialize(key, _hideButtonEntity.IsHide);
            _inputView.OnTapAsObservable()
                .Where(_ => _turnPlayerEntity.Current == TurnPlayer.Self)
                .Subscribe(_inputUseCase.OnTap)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}