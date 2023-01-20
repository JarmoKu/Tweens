using UnityEngine;

namespace JK.Tweening
{
    /*public static class MoveTweenExtensions
    {
        public static MoveTween MoveFromTo (
            this Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            return new MoveTween (transform, start, end, duration, space);
        }

        public static MoveTween MoveTo (
            this Transform transform, Vector3 end, float duration, Space space = Space.Self)
        {
            var start = space.Equals (Space.Self) ? transform.localPosition : transform.position;
            return new MoveTween (transform, start, end, duration, space);
        }

        public static MoveTween MoveFrom (
            this Transform transform, Vector3 start, float duration, Space space = Space.Self)
        {
            var end = space.Equals (Space.Self) ? transform.localPosition : transform.position;
            return new MoveTween (transform, start, end, duration, space);
        }
    }*/
}