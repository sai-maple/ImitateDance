using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Domain.Entity.Common;
using ImitateDance.Scripts.Domain.Entity.Game.Core;
using UniScreen.Container;
using VContainer.Unity;

namespace ImitateDance.Scripts.Presentation.Presenter.Game.Core
{
    public sealed class GameInitializePresenter : IInitializable
    {
        private readonly ScreenContainer _screenContainer = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly ScoreEntity _scoreEntity = default;
        private readonly DifficultyEntity _difficultyEntity = default;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public GameInitializePresenter(ScreenContainer screenContainer, MusicEntity musicEntity,
            ScoreEntity scoreEntity, DifficultyEntity difficultyEntity)
        {
            _screenContainer = screenContainer;
            _musicEntity = musicEntity;
            _scoreEntity = scoreEntity;
            _difficultyEntity = difficultyEntity;
        }

        public async void Initialize()
        {
            // 譜面のロード　まで完了後 UIを呼び出す
            await _musicEntity.Initialize(_difficultyEntity.Value, _cancellation.Token);
            _scoreEntity.Initialize(_musicEntity.Score, _musicEntity.Next, _difficultyEntity.Value);
            _screenContainer.Push("Intro", token: _cancellation.Token).Forget();
        }
    }
}