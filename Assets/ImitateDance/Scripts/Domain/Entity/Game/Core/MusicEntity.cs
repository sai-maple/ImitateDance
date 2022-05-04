using System;
using System.Collections.Generic;
using ImitateDance.Scripts.Applications.Data;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class MusicEntity : IDisposable
    {
        public float DanceTime { get; private set; }
        public float AudienceTime { get; private set; }
        public ScoreDto Score => _score[_index];

        private readonly Subject<Unit> _subject = default;
        private List<ScoreDto> _score = default;
        private int _index = default;

        public MusicEntity()
        {
            _subject = new Subject<Unit>();
            _index = -1;
            DanceTime = 1;
            AudienceTime = 1;
            _score = new List<ScoreDto>()
            {
                new ScoreDto(new List<NoteDto>())
            };
        }

        public IObservable<Unit> OnFinishAsObservable()
        {
            return _subject.Share();
        }

        // json のロード　パース　曲のBPMを元に読み込む, 譜面のパース
        public void Initialize()
        {
            // 最初Nextから呼ばれるので初期値は-1にする
            _index = -1;
            DanceTime = 1;
            AudienceTime = 1;
            _score = new List<ScoreDto>()
            {
                new ScoreDto(new List<NoteDto>())
            };
        }

        // 次のターンの譜面をセット
        public bool TryNext()
        {
            _index++;
            return _index < _score.Count;
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}