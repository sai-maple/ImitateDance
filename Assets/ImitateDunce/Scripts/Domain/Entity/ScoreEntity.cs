using System;
using System.Collections.Generic;
using ImitateDunce.Applications.Data;
using ImitateDunce.Applications.Enums;
using UniRx;

namespace ImitateDunce.Domain.Entity
{
    public sealed class ScoreEntity : IDisposable
    {
        private readonly Subject<DunceData> _subject = default;
        private readonly Dictionary<int, Dunce> _demo = default;
        private readonly Dictionary<int, Dunce> _dunce = default;
        private ScoreDto _score = default;

        public ScoreEntity()
        {
            _subject = new Subject<DunceData>();
        }

        // タップしたタイミングのNoteを返す
        public IObservable<DunceData> OnDunceAsObservable()
        {
            return _subject.Share();
        }

        // demo dunceの中身を空の譜面にする
        public void SetScore(ScoreDto score)
        {
            _score = score;
            _demo.Clear();
            _dunce.Clear();
        }

        // 閾値以内のnoteに入力した方向をセットする
        public void OnDemo(float time, Dunce demo)
        {
            foreach (var note in _score.Score)
            {
                if (MathF.Abs(note.Time - time) > 0.1f) continue;
                if (_demo.ContainsKey(note.Beat)) continue;
                _demo.Add(note.Beat, demo);
                _subject.OnNext(new DunceData(note.Beat, demo));
                break;
            }
        }

        // 閾値以内のnoteに入力した方向をセットして得点を返す
        public int OnDunce(float time, Dunce dunce)
        {
            var point = 0;
            foreach (var note in _score.Score)
            {
                // todo threshold and point
                if (MathF.Abs(note.Time - time) > 0.1f) continue;
                if (_dunce.ContainsKey(note.Beat)) continue;
                _dunce.Add(note.Beat, dunce);
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : Dunce.Non;
                _subject.OnNext(new DunceData(note.Beat, demo, dunce));
                break;
            }

            return point;
        }

        public bool IsPerfect()
        {
            foreach (var note in _score.Score)
            {
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : Dunce.Non;
                if (!_dunce.ContainsKey(note.Beat)) return false;
                if (demo != _dunce[note.Beat]) return false;
            }

            return true;
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}