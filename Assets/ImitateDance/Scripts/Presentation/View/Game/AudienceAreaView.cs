using System;
using System.Collections.Generic;
using System.Linq;
using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class AudienceAreaView : MonoBehaviour
    {
        [SerializeField] private AudienceView _origin = default;

        private float _threshold = default;
        private readonly List<AudienceView> _audienceViews = new List<AudienceView>();

        public void Initialize(float threshold)
        {
            _threshold = threshold;
            for (var i = 0; i < 20; i++)
            {
                var prefab = _origin.Create(transform);
                prefab.SetActive(false);
                _audienceViews.Add(prefab);
            }
        }

        public void ChangePointSelf(int self, int opponent)
        {
            if (_audienceViews.All(audience => audience.IsActive))
            {
                ChangeAudience((float)self / (self + opponent), TurnPlayer.Self);
                return;
            }

            AddAudience(Mathf.FloorToInt(self / _threshold), TurnPlayer.Self);
        }

        public void ChangePointOpponent(int self, int opponent)
        {
            if (_audienceViews.All(audience => audience.IsActive))
            {
                ChangeAudience((float)opponent / (self + opponent), TurnPlayer.Opponent);
                return;
            }

            AddAudience(Mathf.FloorToInt(opponent / _threshold), TurnPlayer.Opponent);
        }

        private void AddAudience(int point, TurnPlayer target)
        {
            var currentNum = _audienceViews.Where(audience => audience.IsActive)
                .Count(audience => audience.SupportPlayer == target);
            var addNum = point - currentNum;

            foreach (var audience in _audienceViews.Where(audience => !audience.IsActive).Take(addNum))
            {
                currentNum++;
                audience.SetActive(true);
                audience.ChangeSupportPlayer(target, RandomPosition(target, currentNum));
            }
        }

        // 割合ではほとんど変化しないので、45~55%の範囲でめっちゃ重みをかける
        private void ChangeAudience(float percentage, TurnPlayer target)
        {
            var diff = Mathf.Clamp(((percentage - 0.5f) * 100), -5, 5);
            var targetAudienceNum = Mathf.FloorToInt(_audienceViews.Count / 2f + diff);
            var currentNum = _audienceViews.Where(audience => audience.IsActive)
                .Count(audience => audience.SupportPlayer == target);
            var changeNum = targetAudienceNum - currentNum;

            if (changeNum <= 0) return;

            var changePlayers = _audienceViews
                .Where(audience => audience.SupportPlayer == target.Reverse())
                .TakeLast(changeNum);
            foreach (var audienceView in changePlayers)
            {
                currentNum++;
                audienceView.ChangeSupportPlayer(target, RandomPosition(target, currentNum));
            }
        }

        private static Vector3 RandomPosition(TurnPlayer supportPlayer, int index)
        {
            var offset = supportPlayer switch
            {
                TurnPlayer.Self => new Vector3(-5, 0),
                TurnPlayer.Opponent => new Vector3(5, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(supportPlayer), supportPlayer, null)
            };

            var i = (index % 11);
            var col = i < 6 ? i - 2.5f : (i - 5) - 2f;
            var step = Mathf.FloorToInt(index / 11f);
            var row = i < 6 ? step * -0.3f : (step + 1) * -0.3f;

            offset.x += col + Random.Range(-0.1f, 0.1f);
            offset.y += row + Random.Range(-0.1f, 0.1f);

            return offset;
        }

        public void SetSpeed(float speed)
        {
            foreach (var audience in _audienceViews)
            {
                audience.SetSpeed(speed);
            }
        }

        public void Finish()
        {
            foreach (var audience in _audienceViews)
            {
                audience.Finish();
            }
        }

        public void Result(TurnPlayer winPlayer)
        {
            foreach (var audience in _audienceViews)
            {
                audience.Result(winPlayer);
            }
        }
    }
}