using System;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.UseCase.Game.Core;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Phase
{
    public sealed class TutorialPresenter : IInitializable
    {
        private readonly TurnUseCase _turnUseCase = default;

        public TutorialPresenter(TurnUseCase turnUseCase)
        {
            _turnUseCase = turnUseCase;
        }

        public async void Initialize()
        {
            // tutorial
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            // 十字キーでアタシのダンスについてきなさい的なセリフ
            // Tutorialの後に再生
            _turnUseCase.GameStart();
        }
    }
}