using System.Threading;
using Cysharp.Threading.Tasks;

namespace ImitateDunce.Domain.Entity
{
    public sealed class TimeEntity
    {
        public float Time { get; private set; }

        public async UniTask StartAsync(float limit, CancellationToken token)
        {
            Time = 0;
            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                if (token.IsCancellationRequested) return;
                if (Time < limit) break;
            }
        }

        public void FixUpdate(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}