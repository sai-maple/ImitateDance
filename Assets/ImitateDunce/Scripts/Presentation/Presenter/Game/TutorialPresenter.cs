using ImitateDunce.Domain.UseCase.Game.Core;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game
{
    public sealed class TutorialPresenter : IInitializable
    {
        private readonly TurnUseCase _turnUseCase = default;
        
        public void Initialize()
        {
            // 十字キーでアタシのダンスについてきなさい的なセリフ
            // Tutorialの後に再生
            _turnUseCase.GameStart();
        }
    }
}