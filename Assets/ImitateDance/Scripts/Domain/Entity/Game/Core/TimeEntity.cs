using System.Threading;
using Cysharp.Threading.Tasks;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class TimeEntity
    {
        public float Time { get; private set; }

        public async UniTask DanceAsync(float limit, CancellationToken token)
        {
            Time = 0;
            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                if (token.IsCancellationRequested) return;
                if (Time > limit) break;
            }
        }

        // delta time * speed 
        public void FixUpdate(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}