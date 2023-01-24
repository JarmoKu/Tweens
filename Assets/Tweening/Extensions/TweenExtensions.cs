using System;
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
            var start = transform.GetPosition (space);
            return new MoveTween (transform, start, end, duration, space);
        }

        public static MoveTween MoveFrom (this Transform transform, Vector3 start, float duration, Space space = Space.Self)
        {
            var end = transform.GetPosition (space);
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
            var endRotation = transform.GetRotation (space);
            return new RotateTween (transform, start, endRotation, duration, space);
        }

        public static RotateTween RotateTo (this Transform transform, Vector3 end, float duration, Space space = Space.Self)
        {
            var startRotation = transform.GetRotation (space);
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
            var endPosition = transform.GetPosition (space);
            var center = Vector3.Lerp (start, endPosition, 0.5f);
            var offset = peak - center;
            peak += offset;

            return new JumpTween (transform, start, peak, endPosition, duration, space);
        }

        public static JumpTween JumpTo (this Transform transform, Vector3 peak, Vector3 end, float duration, Space space = Space.Self)
        {
            var startPosition = transform.GetPosition (space);
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
            var endPosition = transform.GetPosition (space);
            var peak = Vector3.Lerp (start, endPosition, 0.5f) + up.normalized * (height * 2f);
            return new JumpTween (transform, start, peak, endPosition, duration, space);
        }

        public static JumpTween JumpTo (this Transform transform, Vector3 end, float height, float duration, Vector3 up, Space space = Space.Self)
        {
            var startPosition = transform.GetPosition (space);
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

        #region Numbers
        public static FloatTween TweenNumber (this float startValue, float endValue, float duration, Action<float> callback)
        {
            return new FloatTween (startValue, endValue, duration, callback); ;
        }

        public static FloatTween TweenNumber (this float startValue, float endValue, float duration, float increment, Action<float> callback)
        {
            return new FloatTween (startValue, endValue, duration, increment, callback); ;
        }

        public static IntTween TweenNumber (this int startValue, int endValue, float duration, Action<int> callback)
        {
            return new IntTween (startValue, endValue, duration, callback); ;
        }

        public static IntTween TweenNumber (this int startValue, int endValue, float duration, int increment, Action<int> callback)
        {
            return new IntTween (startValue, endValue, duration, increment, callback); ;
        }
        #endregion

        #region Transform
        public static void SetPosition (this Transform transform, Vector3 position, Space space)
        {
            if (space.Matches (Space.Self))
                transform.localPosition = position;
            else
                transform.position = position;
        }

        public static void SetRotation (this Transform transform, Vector3 rotation, Space space)
        {
            if (space.Matches (Space.Self))
                transform.localRotation = Quaternion.Euler (rotation);
            else
                transform.rotation = Quaternion.Euler (rotation);
        }

        public static Vector3 GetPosition (this Transform transform, Space space)
        {
            return space.Matches (Space.World) ? transform.position : transform.localPosition;
        }

        public static Vector3 GetRotation (this Transform transform, Space space)
        {
            return space.Matches (Space.World) ? transform.eulerAngles : transform.localEulerAngles;
        }
        #endregion

        #region Enums
        public static bool Matches (this Space easeType, Space comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this LoopType easeType, LoopType comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this TweenClass easeType, TweenClass comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this TweenType easeType, TweenType comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this ImageTweenType easeType, ImageTweenType comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this NumberTweens easeType, NumberTweens comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this PlayOn easeType, PlayOn comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this PunchType easeType, PunchType comparison)
        {
            return (int)easeType == (int)comparison;
        }

        public static bool Matches (this EaseType easeType, EaseType comparison)
        {
            return (int)easeType == (int)comparison;
        }
        #endregion
    }
}