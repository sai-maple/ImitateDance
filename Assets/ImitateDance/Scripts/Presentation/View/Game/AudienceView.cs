using DG.Tweening;
using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class AudienceView : MonoBehaviour
    {
        [SerializeField] private Animator _animator = default;
        [SerializeField] private SpriteRenderer _body = default;
        [SerializeField] private SpriteRenderer[] _renderers = default;
        private static readonly int Win = Animator.StringToHash("Win");
        private static readonly int Lose = Animator.StringToHash("Lose");
        private static readonly int FinishHash = Animator.StringToHash("Finish");

        public TurnPlayer SupportPlayer { get; private set; }
        public bool IsActive => gameObject.activeSelf;

        public AudienceView Create(Transform content)
        {
            return Instantiate(this, content);
        }

        private void Awake()
        {
            var bodies = new Color[]
            {
                new Color(0.2f, 0.2f, 0.2f),
                new Color(0.1f, 0.1f, 0.1f),
                new Color(0f, 0f, 0f),
            };
            _body.color = bodies[Random.Range(0, bodies.Length)];

            var colors = new Color[]
            {
                new Color(0.21f, 0.9f, 1f),
                new Color(1f, 0.98f, 0.11f),
                new Color(1f, 0.32f, 0.32f),
                new Color(0.5f, 1f, 0.27f),
                new Color(0.83f, 0.23f, 1f),
            };
            var color = colors[Random.Range(0, colors.Length)];
            foreach (var sprite in _renderers)
            {
                sprite.color = color;
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetSpeed(float speed)
        {
            _animator.speed = speed;
        }

        public void ChangeSupportPlayer(TurnPlayer supportPlayer, Vector3 position)
        {
            SupportPlayer = supportPlayer;
            transform.DOLocalMove(position, 2f);
        }

        public void Finish()
        {
            _animator.SetTrigger(FinishHash);
        }

        public void Result(TurnPlayer winPlayer)
        {
            _animator.SetTrigger(winPlayer == SupportPlayer ? Win : Lose);
        }
    }
}