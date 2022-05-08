using System;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Presentation.View.Common;
using UniRx;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.UI
{
    public sealed class HideButtonPresenter : IInitializable, IDisposable
    {
        private readonly HideButtonEntity _hideEntity = default;
        private readonly HideButtonToggle _hideButtonToggle = default;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public HideButtonPresenter(HideButtonEntity hideEntity, HideButtonToggle hideButtonToggle)
        {
            _hideEntity = hideEntity;
            _hideButtonToggle = hideButtonToggle;
        }

        public void Initialize()
        {
            _hideButtonToggle.Initialize(_hideEntity.IsHide);
            _hideButtonToggle.OnChangeAsObservable()
                .Subscribe(_hideEntity.Set)
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}