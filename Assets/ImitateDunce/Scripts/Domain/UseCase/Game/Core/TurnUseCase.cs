using System.Collections.Generic;
using ImitateDunce.Applications.Data;
using ImitateDunce.Domain.Entity.Game.Core;

namespace ImitateDunce.Domain.UseCase.Game.Core
{
    public sealed class TurnUseCase
    {
        private readonly PhaseEntity _phaseEntity = default;
        private readonly ScoreEntity _scoreEntity = default;
        private readonly SpeedEntity _speedEntity = default;
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
        public void GameStart()
        {
            _turnPlayerEntity.GameStart();
            // todo
            _scoreEntity.SetScore(new ScoreDto(new List<NoteDto>()));
            _phaseEntity.GameStart();
        }

        // 曲の再生終わり(Timerのlimitで呼ばれる)
        public void TurnChange()
        {
            // パーフェクトのスコア計算
            _scoreEntity.IsPerfect();
            _turnPlayerEntity.NextTurn();
            _scoreEntity.SetScore(new ScoreDto(new List<NoteDto>()));
            _phaseEntity.Next();
            // 偶数の時呼ぶ
            // _speedEntity.SpeedUp();
        }
    }
}