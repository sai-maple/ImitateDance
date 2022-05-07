using System;
using System.Collections.Generic;
using System.Linq;
using ImitateDance.Scripts.Applications.Common;
using Random = UnityEngine.Random;

namespace ImitateDance.Scripts.Applications.Enums
{
    [Flags]
    public enum DanceDirection
    {
        Non = Up | Down | Right | Left,
        Up = 1 << 0,
        Down = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3
    }

    public static class DanceDirectionExtension
    {
        private static readonly List<DanceDirection> Directions = new List<DanceDirection>()
        {
            DanceDirection.Up, DanceDirection.Down, DanceDirection.Right, DanceDirection.Left
        };

        public static DanceDirection RandomOne()
        {
            return Directions[Random.Range(0, 4)];
        }

        public static DanceDirection CpuTap(this DanceDirection direction)
        {
            if (Directions.All(d => d == (d & direction))) RandomOne();

            return Random.Range(0, 100) < 90 ? direction : RandomOne();
        }
    }
}