using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ImitateDunce.Domain.Entity.Game.Core
{
    public sealed class TimeEntity
    {
        // todo limit と　Audienceの時間を曲選択時に計算する
        public float Time { get; private set; }
        public float Limit { get; private set; }

        public void Initialize()
        {
            
        }
        
        public async UniTask DunceAsync(float limit, CancellationToken token)
        {
            Time = 0;
            Limit = limit;
            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                if (token.IsCancellationRequested) return;
                if (Time < limit) break;
            }
        }

        public async UniTask AudienceAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        }

        public void FixUpdate(float deltaTime)
        {
            Time += deltaTime;
        }
    }
}