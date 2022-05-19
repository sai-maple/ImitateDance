using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class KeyConfigEntity
    {
        public KeyCode Up { get; private set; } = KeyCode.UpArrow;
        public KeyCode Down { get; private set; } = KeyCode.DownArrow;
        public KeyCode Right { get; private set; } = KeyCode.RightArrow;
        public KeyCode Left { get; private set; } = KeyCode.LeftArrow;

        public void ChangeUp(KeyCode keyCode)
        {
            Up = keyCode;
        }
        
        public void ChangeDown(KeyCode keyCode)
        {
            Down = keyCode;
        }
        
        public void ChangeRight(KeyCode keyCode)
        {
            Right = keyCode;
        }
        
        public void ChangeLeft(KeyCode keyCode)
        {
            Left = keyCode;
        }
    }
}