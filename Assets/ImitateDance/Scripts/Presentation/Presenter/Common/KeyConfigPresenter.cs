using System;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Common
{
    public sealed class KeyConfigPresenter : IInitializable, IDisposable
    {
        private readonly KeyConfigEntity _keyConfigEntity = default;
        private readonly KeyConfigView _view = default;
        private readonly DanceDirection _direction = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public KeyConfigPresenter(KeyConfigEntity keyConfigEntity, KeyConfigView view, DanceDirection direction)
        {
            _keyConfigEntity = keyConfigEntity;
            _view = view;
            _direction = direction;
        }

        public void Initialize()
        {
            var defaultKey = _keyConfigEntity.GetKey(_direction);
            _view.Initialize(defaultKey);

            _view.OnChangeAsObservable()
                .Subscribe(key =>
                {
                    _keyConfigEntity.OnChance(_direction, key);
                    _view.OnChanged(_keyConfigEntity.GetKey(_direction));
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}