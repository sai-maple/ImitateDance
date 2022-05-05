using System;
using System.Collections.Generic;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class ScoreEntity : IDisposable
    {
        private readonly Subject<DanceData> _subject = default;
        private readonly Subject<ScoreData> _scoreSubject = default;
        private readonly Dictionary<int, DanceDirection> _demo = default;
        private readonly Dictionary<int, DanceDirection> _dunce = default;
        private ScoreData _score = default;

        public ScoreEntity()
        {
            _subject = new Subject<DanceData>();
            _scoreSubject = new Subject<ScoreData>();
            _demo = new Dictionary<int, DanceDirection>();
            _dunce = new Dictionary<int, DanceDirection>();
        }

        public IObservable<ScoreData> OnScoreAsObservable()
        {
            return _scoreSubject.Share();
        }

        // タップしたタイミングのNoteを返す
        public IObservable<DanceData> OnDanceAsObservable()
        {
            return _subject.Share();
        }

        // demo dunceの中身を空の譜面にする
        public void SetScore(ScoreData score)
        {
            _score = score;
            _demo.Clear();
            _dunce.Clear();
        }

        // 閾値以内のnoteに入力した方向をセットする
        public void OnDemo(float time, DanceDirection demo)
        {
            foreach (var note in _score.Score)
            {
                if (Mathf.Abs(note.Time - time) > 0.1f) continue;
                if (_demo.ContainsKey(note.Beat)) continue;
                _demo.Add(note.Beat, demo);
                _subject.OnNext(new DanceData(note.Beat, demo));
                break;
            }
        }

        // 閾値以内のnoteに入力した方向をセットして得点を返す
        public int OnDance(float time, DanceDirection danceDirection)
        {
            var point = 0;
            foreach (var note in _score.Score)
            {
                // todo threshold and point
                if (Mathf.Abs(note.Time - time) > 0.1f) continue;
                if (_dunce.ContainsKey(note.Beat)) continue;
                _dunce.Add(note.Beat, danceDirection);
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : DanceDirection.Non;
                _subject.OnNext(new DanceData(note.Beat, demo, danceDirection));
                break;
            }

            return point;
        }

        public bool IsPerfect()
        {
            foreach (var note in _score.Score)
            {
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : DanceDirection.Non;
                if (!_dunce.ContainsKey(note.Beat)) return false;
                if (demo != _dunce[note.Beat]) return false;
            }

            return true;
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
            _scoreSubject?.OnCompleted();
            _scoreSubject?.Dispose();
        }
    }
}