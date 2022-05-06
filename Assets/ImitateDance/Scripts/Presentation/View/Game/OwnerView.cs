using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;
using Logger = ImitateDance.Scripts.Applications.Common.Logger;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class OwnerView : MonoBehaviour
    {
        [SerializeField] private Animator _demoAnimator = default;
        [SerializeField] private Animator _dunceAnimator = default;

        private static readonly int SelfHash = Animator.StringToHash("Self");
        private static readonly int OpponentHash = Animator.StringToHash("Opponent");

        public void ChangeState(DancePhase phase, TurnPlayer turnPlayer)
        {
            Logger.Log($"{phase} : {turnPlayer}");
            var demo = turnPlayer == TurnPlayer.Self ? SelfHash : OpponentHash;
            var dunce = turnPlayer == TurnPlayer.Self ^ phase == DancePhase.Dance
                ? OpponentHash
                : SelfHash;
            _demoAnimator.SetTrigger(demo);
            _dunceAnimator.SetTrigger(dunce);
        }
    }
}