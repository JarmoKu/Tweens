using UnityEngine;

namespace JK.Tweening
{
    /*public static class RotateTweenExtensions
    {
        public static RotateTween RotateFromTo (this Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            return new RotateTween (transform, start, end, duration, space);
        }

        public static RotateTween RotateFrom (this Transform transform, Vector3 start, float duration, Space space = Space.Self)
        {
            var endRotation = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            return new RotateTween (transform, start, endRotation, duration, space);
        }

        public static RotateTween RotateTo (this Transform transform, Vector3 end, float duration, Space space = Space.Self)
        {
            var startRotation = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            return new RotateTween (transform, startRotation, end, duration, space);
        }
    }*/
}