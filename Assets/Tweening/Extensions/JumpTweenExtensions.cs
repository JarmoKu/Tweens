using UnityEngine;

namespace JK.Tweening
{
    /*public static class JumpTweenExtensions
    {
        public static JumpTween JumpFromTo (this Transform transform, Vector3 start, Vector3 peak, Vector3 end, float duration, Space space = Space.Self)
        {
            var center = Vector3.Lerp (start, end, 0.5f);
            var offset = peak - center;
            peak += offset;

            return new JumpTween (transform, start, peak, end, duration, space);
        }

        public static JumpTween JumpFrom (this Transform transform, Vector3 start, Vector3 peak, float duration, Space space = Space.Self)
        {
            var endPosition = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            var center = Vector3.Lerp (start, endPosition, 0.5f);
            var offset = peak - center;
            peak += offset;

            return new JumpTween (transform, start, peak, endPosition, duration, space);
        }

        public static JumpTween JumpTo (this Transform transform, Vector3 peak, Vector3 end, float duration, Space space = Space.Self)
        {
            var startPosition = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            var center = Vector3.Lerp (startPosition, end, 0.5f);
            var offset = peak - center;
            peak += offset;

            return new JumpTween (transform, startPosition, peak, end, duration, space);
        }

        public static JumpTween JumpFromTo (this Transform transform, Vector3 start, Vector3 end, float height, float duration, Vector3 up, Space space = Space.Self)
        {
            var peak = Vector3.Lerp (start, end, 0.5f) + (height * 2f) * up.normalized;
            return new JumpTween (transform, start, peak, end, duration, space);
        }

        public static JumpTween JumpFrom (this Transform transform, Vector3 start, float height, float duration, Vector3 up, Space space = Space.Self)
        {
            var endPosition = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            var peak = Vector3.Lerp (start, endPosition, 0.5f) + up.normalized * (height * 2f);
            return new JumpTween (transform, start, peak, endPosition, duration, space);
        }

        public static JumpTween JumpTo (this Transform transform, Vector3 end, float height, float duration, Vector3 up, Space space = Space.Self)
        {
            var startPosition = space.Equals (Space.Self) ? transform.localEulerAngles : transform.eulerAngles;
            var peak = Vector3.Lerp (startPosition, end, 0.5f) + up.normalized * (height * 2f);
            return new JumpTween (transform, startPosition, peak, end, duration, space);
        }
    }*/
}