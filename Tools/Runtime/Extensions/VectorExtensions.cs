using UnityEngine;
using DivineSkies.Tools.Helper;

namespace DivineSkies.Tools.Extensions
{
    public static class VectorExtensions
    {
        public static Direction ToDirection(this Vector2 vec)
        {
            if (Mathf.Abs(vec.y) >= Mathf.Abs(vec.x))
                return vec.y > 0 ? Direction.Up : (vec.y < 0 ? Direction.Down : Direction.Null);
            if (Mathf.Abs(vec.y) < Mathf.Abs(vec.x))
                return vec.x > 0 ? Direction.Right : (vec.x < 0 ? Direction.Left : Direction.Null);
            return Direction.Null;
        }

        public static Vector2 ToVector2(this Direction dir) => dir switch
        {
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            _ => throw new System.NotImplementedException(dir + " can not be converted to vector2")
        };

        public static Direction Invert(this Direction dir)
        {
            if (dir == Direction.Left)
                return Direction.Right;
            if (dir == Direction.Right)
                return Direction.Left;
            if (dir == Direction.Up)
                return Direction.Down;
            if (dir == Direction.Down)
                return Direction.Up;
            return Direction.Null;
        }

        public static Vector2 Abs(this Vector2 vec)
        {
            return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
        }
    }
}