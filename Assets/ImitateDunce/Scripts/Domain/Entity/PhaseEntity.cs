using System;
using ImitateDunce.Applications.Enums;
using UniRx;

namespace ImitateDunce.Domain.Entity
{
    public class PhaseEntity : IDisposable
    {
        private readonly Subject<DuncePhase> _subject;
        public DuncePhase Current { get; private set; }

        public PhaseEntity()
        {
            _subject = new Subject<DuncePhase>();
            Current = DuncePhase.EnemyDunce;
        }

        public IObservable<DuncePhase> OnChangeAsObservable()
        {
            return _subject.Share();
        }

        public void Next()
        {
            Current = Current.Next();
            _subject.OnNext(Current);
        }

        public void Dispose()
        {
            _subject?.OnCompleted();
            _subject?.Dispose();
        }
    }
}