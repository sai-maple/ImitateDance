using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace ImitateDance.Scripts.Presentation.View.Game
{
    public sealed class NoteView : MonoBehaviour
    {
        [SerializeField] private Animator _directionAnimator = default;
        [SerializeField] private Image _emptyImage = default;
        [SerializeField] private Image _nonEmptyImage = default;
        [SerializeField] private Image _missImage = default;
        private static readonly int EmptyHash = Animator.StringToHash("Empty");

        public void Initialize(bool isNotEmpty)
        {
            _emptyImage.enabled = !isNotEmpty;
            _nonEmptyImage.enabled = isNotEmpty;
            _missImage.enabled = false;
            _directionAnimator.SetTrigger(EmptyHash);
        }

        public void Judge(DanceDirection dance, bool isSuccess)
        {
            _directionAnimator.SetTrigger(dance.ToString());
            _nonEmptyImage.enabled = false;
            _missImage.enabled = !isSuccess;
        }
    }
}