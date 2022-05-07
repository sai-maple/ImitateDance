using UniScreen.Container;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Screen
{
    public sealed class ScreenInitializePresenter : IInitializable
    {
        private readonly ScreenContainer _screenContainer = default;

        public ScreenInitializePresenter(ScreenContainer screenContainer)
        {
            _screenContainer = screenContainer;
        }

        public async void Initialize()
        {
            await _screenContainer.NewScreen("ScreenEmpty");
            await _screenContainer.Push("Title");
        }
    }
}