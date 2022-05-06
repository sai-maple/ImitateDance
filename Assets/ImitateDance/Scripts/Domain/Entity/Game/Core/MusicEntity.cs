using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class MusicEntity : IDisposable
    {
        public float DanceTime { get; private set; }
        public float HalfBarTime { get; private set; }
        public NotesDto Score => _score[_index];
        public NotesDto Next => _index + 1 < _score.Count ? _score[_index + 1] : new NotesDto(new List<NoteDto>());

        private readonly Subject<Unit> _subject = default;
        private List<NotesDto> _score = default;
        private int _index = default;

        public MusicEntity()
        {
            _subject = new Subject<Unit>();
            DanceTime = 1;
        }

        public IObservable<Unit> OnFinishAsObservable()
        {
            return _subject.Share();
        }

        // json のロード　パース　曲のBPMを元に読み込む, 譜面のパース
        public async UniTask Initialize(MusicDifficulty difficulty, CancellationToken token = default)
        {
            // 最初Nextから呼ばれるので初期値は-1にする
            var textAsset = await Addressables.LoadAssetAsync<TextAsset>($"Score{difficulty}").WithCancellation(token);
            var score = JsonUtility.FromJson<ScoreDto>(textAsset.text);
            _score = score.Scores;
            _index = 0;
            var beetTime = 60f / score.Bpm;
            DanceTime = beetTime * 8;
            HalfBarTime = beetTime / 2;
        }

        // 次のターンの譜面をセット
        public bool TryNext()
        {
            _index++;
            var hasNext = _index < _score.Count;
            if (!hasNext) _subject.OnNext(Unit.Default);
            return hasNext;
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}