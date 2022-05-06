using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Applications.Enums;
using ImitateDance.Scripts.Domain.Entity.Game.Core;

namespace ImitateDance.Scripts.Domain.UseCase.Game.Core
{
    public sealed class TurnUseCase
    {
        private readonly PhaseEntity _phaseEntity = default;
        private readonly ScoreEntity _scoreEntity = default;
        private readonly TimeEntity _timeEntity = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly PointEntity _pointEntity = default;
        private readonly TurnPlayerEntity _turnPlayerEntity = default;

        public TurnUseCase(PhaseEntity phaseEntity, ScoreEntity scoreEntity,
            TimeEntity timeEntity, MusicEntity musicEntity, PointEntity pointEntity, TurnPlayerEntity turnPlayerEntity)
        {
            _phaseEntity = phaseEntity;
            _scoreEntity = scoreEntity;
            _timeEntity = timeEntity;
            _musicEntity = musicEntity;
            _pointEntity = pointEntity;
            _turnPlayerEntity = turnPlayerEntity;
        }

        // ゲーム開始時にTurnChangeの代わりに1度だけ呼ばれる
        // Audienceを挟む都合1拍遅れるので曲もずらす
        public void GameStart()
        {
            _scoreEntity.Initialize(_musicEntity.Score, _musicEntity.Next);
            _turnPlayerEntity.GameStart();
            _phaseEntity.GameStart();
            _timeEntity.GameStart();
        }

        public async UniTask OnDance(CancellationToken token)
        {
            _scoreEntity.OnStartPhase();
            await _timeEntity.DanceAsync(_musicEntity.DanceTime, token);
            if (token.IsCancellationRequested) return;
            // 譜面の最後に到達したら終了
            if (!_musicEntity.TryNext()) return;
            _scoreEntity.SetScore(_musicEntity.Next);
            _phaseEntity.Next(DancePhase.Demo);
        }

        public async UniTask OnDemo(CancellationToken token)
        {
            _scoreEntity.OnStartPhase();
            await _timeEntity.DanceAsync(_musicEntity.DanceTime, token);
            if (token.IsCancellationRequested) return;
            // パーフェクトのスコア計算
            _pointEntity.Bonus(_turnPlayerEntity.Current, _scoreEntity.IsPerfect());
            _turnPlayerEntity.NextTurn();
            _phaseEntity.Next(DancePhase.Dance);
        }
    }
}