using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class AudienceView : MonoBehaviour
    {
        [SerializeField] private Animator _selfAudienceAnimator = default;
        [SerializeField] private Animator _opponentAudienceAnimator = default;
        private static readonly int Point = Animator.StringToHash("Point");

        public void SetSpeed(float speed)
        {
            _selfAudienceAnimator.speed = speed;
            _opponentAudienceAnimator.speed = speed;
        }

        public void SetSelfPointNormalize(float normalize)
        {
            _selfAudienceAnimator.SetFloat(Point, normalize);
        }

        public void SetOpponentPointNormalize(float normalize)
        {
            _opponentAudienceAnimator.SetFloat(Point, normalize);
        }
    }
}