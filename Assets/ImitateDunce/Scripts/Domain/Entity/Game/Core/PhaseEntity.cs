using System;
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
        }

        public void Next(DuncePhase phase)
        {
            Current = phase;
            _subject.OnNext(Current);
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}