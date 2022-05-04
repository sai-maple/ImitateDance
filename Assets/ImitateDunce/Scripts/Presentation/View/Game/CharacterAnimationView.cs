using ImitateDunce.Applications.Enums;
using UnityEngine;

namespace ImitateDunce.Presentation.View.Game
{
    public sealed class CharacterAnimationView : MonoBehaviour
    {
        [SerializeField] private Animator _animator = default;
        [SerializeField] private TurnPlayer _selfTurn = default;
        private static readonly int WinHash = Animator.StringToHash("Win");
        private static readonly int LoseHash = Animator.StringToHash("Lose");

        public void Dunce(DunceDirection direction, TurnPlayer turnPlayer)
        {
            if (turnPlayer != _selfTurn) return;
            _animator.SetTrigger(direction.ToString());
        }

        public void Result(bool isWin)
        {
            _animator.SetTrigger(isWin ? WinHash : LoseHash);
        }
    }
}