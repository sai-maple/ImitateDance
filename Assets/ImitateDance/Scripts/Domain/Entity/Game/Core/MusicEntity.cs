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
        public float AudienceTime { get; private set; }
        public ScoreData Score => _score[_index];

        private readonly Subject<Unit> _subject = default;
        private List<ScoreData> _score = default;
        private int _index = default;

        public MusicEntity()
        {
            _subject = new Subject<Unit>();
            _index = -1;
            DanceTime = 1;
            AudienceTime = 1;
        }

        public IObservable<Unit> OnFinishAsObservable()
        {
            return _subject.Share();
        }

        // json のロード　パース　曲のBPMを元に読み込む, 譜面のパース
        public async UniTask Initialize(MusicDifficulty difficulty, CancellationToken token = default)
        {
            // 最初Nextから呼ばれるので初期値は-1にする
            var textAsset = await Addressables.LoadAssetAsync<TextAsset>($"Score_{difficulty}").WithCancellation(token);
            var score = JsonUtility.FromJson<ScoreDto>(textAsset.text);
            _score = score.Scores;
            _index = -1;
            DanceTime = (60f / score.Bpm) * 7;
            AudienceTime = 60f / score.Bpm;
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