using DG.Tweening;
using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class AngleView : MonoBehaviour
    {
        [SerializeField] private float _target = default;

        public void DoMove(TurnPlayer turnPlayer)
        {
            var target = turnPlayer == TurnPlayer.Self ? _target : -1 * _target;
            transform.DOLocalMoveX(target, 0.2f);
        }

        public void Reset()
        {
            transform.DOLocalMoveX(0, 0.2f);
        }
    }
}