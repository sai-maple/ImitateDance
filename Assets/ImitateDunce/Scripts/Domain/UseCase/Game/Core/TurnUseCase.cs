using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDunce.Applications.Enums;
using ImitateDunce.Domain.Entity.Game.Core;

namespace ImitateDunce.Domain.UseCase.Game.Core
{
    public sealed class TurnUseCase
    {
        private readonly PhaseEntity _phaseEntity = default;
        private readonly ScoreEntity _scoreEntity = default;
        private readonly SpeedEntity _speedEntity = default;
        private readonly TimeEntity _timeEntity = default;
        private readonly MusicEntity _musicEntity = default;
        private readonly PointEntity _pointEntity = default;
        private readonly TurnPlayerEntity _turnPlayerEntity = default;

        // todo 楽譜のロード

        public TurnUseCase(PhaseEntity phaseEntity, ScoreEntity scoreEntity,
            SpeedEntity speedEntity, TurnPlayerEntity turnPlayerEntity)
        {
            _phaseEntity = phaseEntity;
            _scoreEntity = scoreEntity;
            _speedEntity = speedEntity;
            _turnPlayerEntity = turnPlayerEntity;
        }

        // ゲーム開始時にTurnChangeの代わりに1度だけ呼ばれる
        // Audienceを挟む都合1拍遅れるので曲もずらす
        public void GameStart()
        {
            _turnPlayerEntity.GameStart();
            _phaseEntity.GameStart();
        }

        // Demoの後,歓声+視点移動と同時に呼ばれる
        public async void OnTurnChange(CancellationToken token)
        {
            // パーフェクトのスコア計算
            _pointEntity.Bonus(_turnPlayerEntity.Current, _scoreEntity.IsPerfect());
            _turnPlayerEntity.NextTurn();
            await _timeEntity.AudienceAsync(_musicEntity.AudienceTime, token);
            if (token.IsCancellationRequested) return;
            _phaseEntity.Next(DuncePhase.Dunce);
        }

        // TurnChangeの後呼ばれる
        public async UniTask OnDunce(CancellationToken token)
        {
            await _timeEntity.DunceAsync(_musicEntity.DunceTime, token);
            if (token.IsCancellationRequested) return;
            _phaseEntity.Next(DuncePhase.Audience);
        }

        // Dunceの後に呼ばれる
        public async void OnAudience(CancellationToken token)
        {
            // 譜面の最後に到達したら終了
            if (!_musicEntity.TryNext()) return;
            _scoreEntity.SetScore(_musicEntity.Score);
            // 偶数の時呼ぶ
            // _speedEntity.SpeedUp();
            await _timeEntity.AudienceAsync(_musicEntity.AudienceTime, token);
            if (token.IsCancellationRequested) return;
            _phaseEntity.Next(DuncePhase.Demo);
        }

        // Audienceの後呼ばれる
        public async UniTask OnDemo(CancellationToken token)
        {
            await _timeEntity.DunceAsync(_musicEntity.DunceTime, token);
            if (token.IsCancellationRequested) return;
            _phaseEntity.Next(DuncePhase.TurnChange);
        }
    }
}