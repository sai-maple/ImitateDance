using System;
using ImitateDunce.Applications.Common;
using ImitateDunce.Applications.Enums;
using UniRx;

namespace ImitateDunce.Domain.Entity.Game.Core
{
    public class PhaseEntity : IDisposable
    {
        private readonly Subject<DuncePhase> _subject;
        public DuncePhase Current { get; private set; }

        public PhaseEntity()
        {
            _subject = new Subject<DuncePhase>();
        }

        public IObservable<DuncePhase> OnChangeAsObservable()
        {
            return _subject.Share();
        }

        public void GameStart()
        {
            Current = DuncePhase.Audience;
            _subject.OnNext(Current);
            Logger.Log(Current);
        }

        public void Next(DuncePhase phase)
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