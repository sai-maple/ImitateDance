using System;
using ImitateDance.Scripts.Applications.Common;
using ImitateDance.Scripts.Applications.Enums;
using UniRx;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public class PhaseEntity : IDisposable
    {
        private readonly Subject<DancePhase> _subject;
        public DancePhase Current { get; private set; }

        public PhaseEntity()
        {
            _subject = new Subject<DancePhase>();
            Current = DancePhase.Demo;
        }

        public IObservable<DancePhase> OnChangeAsObservable()
        {
            return _subject.Share();
        }

        public void GameStart()
        {
            Current = DancePhase.Demo;
            _subject.OnNext(Current);
            Logger.Log(Current);
        }

        public void Next(DancePhase phase)
        {
            Current = phase;
            _subject.OnNext(Current);
            Logger.Log(phase);
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}