using UnityEngine;

namespace JK.Tweening
{
    public static class TweenExtensions
    {
        #region Move
        public static MoveTween MoveFromTo (this Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            return new MoveTween (transform, start, end, duration, space);
        }

        public static MoveTween MoveTo (this Transform transform, Vector3 end, float duration, Space space = Space.Self)
        {
            var start = space.Equals (Space.Self) ? transform.localPosition : transform.position;
            return new MoveTween (transform, start, end, duration, space);
        }

        public static MoveTween MoveFrom (this Transform transform, Vector3 start, float duration, Space space = Space.Self)
        {
            var end = space.Equals (Space.Self) ? transform.localPosition : transform.position;
            return new MoveTween (transform, start, end, duration, space);
        }
        #endregion

        #region Scale
        public static ScaleTween ScaleFromTo (this Transform transform, Vector3 start, Vector3 end, float duration)
        {
            return new ScaleTween (transform, start, end, duration);
        }

        public static ScaleTween ScaleTo (this Transform transform, Vector3 end, float duration)
        {
            return new ScaleTween (transform, transform.localScale, end, duration);
        }

        public static ScaleTween ScaleFrom (this Transform transform, Vector3 start, float duration)
        {
            return new ScaleTween (transform, start, transform.localScale, duration);
        }
        #endregion

        #region Rotate
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
        #endregion

        #region Jump
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
        #endregion

        #region Punch
        public static PunchPositionTween PunchPosition (this Transform transform, Vector3 targetScale, float duration, Space space = Space.World)
        {
            return new PunchPositionTween (transform, targetScale, duration, space);
        }

        public static PunchRotationTween PunchRotation (this Transform transform, Vector3 targetScale, float duration, Space space = Space.World)
        {
            return new PunchRotationTween (transform, targetScale, duration, space);
        }

        public static PunchScaleTween PunchScale (this Transform transform, Vector3 targetScale, float duration)
        {
            return new PunchScaleTween (transform, targetScale, duration);
        }
        #endregion
    }
}