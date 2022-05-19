using System;
using ImitateDance.Scripts.Applications.Enums;
using UnityEngine;

namespace ImitateDance.Scripts.Domain.Entity.Common
{
    public sealed class KeyConfigEntity
    {
        private KeyCode _up = KeyCode.UpArrow;
        private KeyCode _down = KeyCode.DownArrow;
        private KeyCode _right = KeyCode.RightArrow;
        private KeyCode _left = KeyCode.LeftArrow;

        public KeyCode GetKey(DanceDirection direction)
        {
            return direction switch
            {
                DanceDirection.Up => _up,
                DanceDirection.Down => _down,
                DanceDirection.Right => _right,
                DanceDirection.Left => _left,
                DanceDirection.Non => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void OnChance(DanceDirection direction, KeyCode keyCode)
        {
            if (keyCode == _up || keyCode == _down || keyCode == _right || keyCode == _left) return;
            switch (direction)
            {
                case DanceDirection.Non:
                    break;
                case DanceDirection.Up:
                    _up = keyCode;
                    break;
                case DanceDirection.Down:
                    _down = keyCode;
                    break;
                case DanceDirection.Right:
                    _right = keyCode;
                    break;
                case DanceDirection.Left:
                    _left = keyCode;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}