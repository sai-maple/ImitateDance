using System;
using System.Collections.Generic;
using System.Linq;
using ImitateDance.Scripts.Applications.Data;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class ScoreEntity : IDisposable
    {
        private readonly Subject<DanceData> _dunceSubject = default;
        private readonly Subject<NoteData> _noteSubject = default;
        private readonly Dictionary<int, DanceDirection> _demo = default;
        private readonly Dictionary<int, DanceDirection> _dunce = default;
        private readonly float _offset = default;
        private NotesDto _score = default;
        private NotesDto _next = default;
        private int _provability = default;
        private int _index = default;
        private float _cpuPower = default;

        public ScoreEntity()
        {
            _dunceSubject = new Subject<DanceData>();
            _noteSubject = new Subject<NoteData>();
            _demo = new Dictionary<int, DanceDirection>();
            _dunce = new Dictionary<int, DanceDirection>();
            _offset = 0.01667f * 8;
        }

        public IObservable<NoteData> OnScoreAsObservable()
        {
            return _noteSubject.Share();
        }

        // タップしたタイミングのNoteを返す
        public IObservable<DanceData> OnDanceAsObservable()
        {
            return _dunceSubject.Share();
        }

        public void Initialize(NotesDto score, NotesDto next, MusicDifficulty difficulty)
        {
            _score = score;
            _next = next;
            _demo.Clear();
            _dunce.Clear();
            _index = 0;
            _provability = difficulty switch
            {
                MusicDifficulty.Easy => 85,
                MusicDifficulty.Normal => 90,
                MusicDifficulty.Hard => 95,
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
            _cpuPower = difficulty switch
            {
                MusicDifficulty.Easy => 2,
                MusicDifficulty.Normal => 2,
                MusicDifficulty.Hard => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };
        }

        // demo dunceの中身を空の譜面にする
        public void SetScore(NotesDto next)
        {
            _score = _next;
            _next = next;
            _demo.Clear();
            _dunce.Clear();
        }

        public void OnStartPhase()
        {
            _index = 0;
        }

        // dunce の判定範囲をすぎ分から、demoの譜面描画を更新する
        public void UpdateDemo(float time, float halfBarTime)
        {
            if (halfBarTime * _index + _offset > time) return;
            var hasNote = _next.Score.Any(note => note.Beat == _index);
            var data = new NoteData(_index, hasNote);
            _noteSubject.OnNext(data);
            _index++;
        }

        public void UpdateDunce(float time, float halfBarTime)
        {
            if (halfBarTime * _index + _offset * 2 > time) return;
            var note = _score.Score.FirstOrDefault(note => note.Beat == _index);

            var data = _demo.ContainsKey(_index)
                ? new NoteData(_index, note != null, _demo[_index])
                : new NoteData(_index, note != null);
            _noteSubject.OnNext(data);
            _index++;
        }

        // 閾値以内のnoteに入力した方向をセットする
        public int OnDemo(float time, DanceDirection demo)
        {
            var point = 0;
            foreach (var note in _score.Score)
            {
                var abs = Mathf.Abs(note.Time - time);
                if (abs > _offset) continue;
                if (_demo.ContainsKey(note.Beat)) continue;
                point = (int)(100 * (_offset - abs / 2f));
                _demo.Add(note.Beat, demo);
                _dunceSubject.OnNext(new DanceData(note.Beat, demo));
                break;
            }

            return point;
        }

        // 閾値以内のnoteに入力した方向をセットして得点を返す
        public int OnDance(float time, DanceDirection danceDirection)
        {
            var point = 0;
            // _dunceの中身から判別するように変更する
            foreach (var note in _score.Score)
            {
                // todo threshold and point
                var abs = Mathf.Abs(note.Time - time);
                if (abs > _offset) continue;
                if (_dunce.ContainsKey(note.Beat)) continue;
                point = (int)(100 * (_offset - abs / 2f));
                _dunce.Add(note.Beat, danceDirection);
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : DanceDirection.Non;
                point = danceDirection == (demo & danceDirection) ? point : 0;
                _dunceSubject.OnNext(new DanceData(note.Beat, demo, danceDirection));
                break;
            }

            return point;
        }

        public int CPUDemo(float time, DanceDirection demo)
        {
            var point = 0;
            foreach (var note in _score.Score)
            {
                if (note.Time > time) continue;
                if (_demo.ContainsKey(note.Beat)) continue;
                point = (int)(100 * (_offset - Random.Range(0, _offset / _cpuPower)));
                _demo.Add(note.Beat, demo);
                _dunceSubject.OnNext(new DanceData(note.Beat, demo));
                break;
            }

            return point;
        }

        public int CpuDance(float time)
        {
            var point = 0;
            // _dunceの中身から判別するように変更する
            foreach (var note in _score.Score)
            {
                if (note.Time > time) continue;
                if (_dunce.ContainsKey(note.Beat)) continue;
                point = (int)(100 * (_offset - Random.Range(0, _offset / _cpuPower)));
                var demo = _demo.ContainsKey(note.Beat) ? _demo[note.Beat] : DanceDirection.Non;
                var direction = demo.CpuTap(_provability);
                point = direction == (demo & direction) ? point : 0;
                _dunce.Add(note.Beat, direction);
                _dunceSubject.OnNext(new DanceData(note.Beat, demo, direction));
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
            _dunceSubject?.OnCompleted();
            _dunceSubject?.Dispose();
            _noteSubject?.OnCompleted();
            _noteSubject?.Dispose();
        }
    }
}