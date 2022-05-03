using ImitateDunce.Domain.UseCase.Game.Core;
using VContainer.Unity;

namespace ImitateDunce.Presentation.Presenter.Game.Phase
{
    public sealed class TutorialPresenter : IInitializable
    {
        private readonly TurnUseCase _turnUseCase = default;

        public TutorialPresenter(TurnUseCase turnUseCase)
        {
            _turnUseCase = turnUseCase;
        }

        public void Initialize()
        {
            // 十字キーでアタシのダンスについてきなさい的なセリフ
            // Tutorialの後に再生
            _turnUseCase.GameStart();
        }
    }
}