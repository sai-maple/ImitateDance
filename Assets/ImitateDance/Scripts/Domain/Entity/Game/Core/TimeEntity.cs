using System.Threading;
using Cysharp.Threading.Tasks;
using ImitateDance.Scripts.Applications.Common;

namespace ImitateDance.Scripts.Domain.Entity.Game.Core
{
    public sealed class TimeEntity
    {
        public double Time => _internalTime - _previousTime;
        private double _previousTime = default;
        private double _internalTime = default;
        private bool _startGame = default;
        private int _count = default;

        public TimeEntity()
        {
            _startGame = false;
        }

        public void GameStart()
        {
            _startGame = true;
            _internalTime = 0;
            _previousTime = 0;
            _count = 0;
        }

        public async UniTask DanceAsync(float limit, CancellationToken token)
        {
            _previousTime = limit * _count;
            _count++;
            limit *= _count;
            while (true)
            {
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
                if (token.IsCancellationRequested) return;
                if (_internalTime > limit) break;
            }
        }

        // delta time * speed 
        public void FixUpdate(float deltaTime)
        {
            if (!_startGame) return;
            _internalTime += deltaTime;
        }
    }
}