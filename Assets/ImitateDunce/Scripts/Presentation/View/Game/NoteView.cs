using ImitateDunce.Applications.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDunce.Presentation.View.Game
{
    public sealed class NoteView : MonoBehaviour
    {
        [SerializeField] private Animator _directionAnimator = default;
        [SerializeField] private Image _emptyImage = default;
        [SerializeField] private Image _nonEmptyImage = default;
        [SerializeField] private Image _missImage = default;
        private static readonly int HideHash = Animator.StringToHash("Hide");

        public void Initialize(bool isNotEmpty)
        {
            _emptyImage.enabled = !isNotEmpty;
            _nonEmptyImage.enabled = isNotEmpty;
            _missImage.enabled = false;
            _directionAnimator.SetTrigger(HideHash);
        }

        public void Hide()
        {
            _directionAnimator.SetTrigger(HideHash);
        }

        public void Judge(DunceDirection dunce, bool isSuccess)
        {
            _directionAnimator.SetTrigger(dunce.ToString());
            _missImage.enabled = !isSuccess;
        }
    }
}