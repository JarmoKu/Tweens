using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace JK.Tweening.Tests
{
    public static class TestUtilities
    {
        public static IEnumerator WaitForTestCondition (
            Func<bool> condition, string context, float timeout, bool skipFirstFrame = false)
        {
            double timePassed = default;
            var startTime = Time.timeAsDouble;

            // From type tweens need to wait for the transform to move into start position 
            // as target transform is originally at the end position
            if (skipFirstFrame)
                yield return new WaitForDomainReload ();

            while (!condition () && timePassed < timeout)
            {
                yield return new WaitForDomainReload ();
                timePassed = Time.timeAsDouble - startTime;
            }

            Debug.Assert (timePassed < timeout, $"{context} failed after {timePassed} seconds.");
        }

        public static MoveTween GetMoveTweenType (
            TweenType tweenType, Transform transformToTween, Vector3 start, Vector3 end, float duration, Space space)
        {
            return tweenType switch
            {
                TweenType.FromTo => transformToTween.MoveFromTo (start, end, duration, space),
                TweenType.From => transformToTween.MoveFrom (start, duration, space),
                TweenType.To => transformToTween.MoveTo (end, duration, space),
                _ => default,
            };
        }

        public static RotateTween GetRotateTweenType (
            TweenType tweenType, Transform transformToTween, Vector3 start, Vector3 end, float duration, Space space)
        {
            return tweenType switch
            {
                TweenType.FromTo => transformToTween.RotateFromTo (start, end, duration, space),
                TweenType.From => transformToTween.RotateFrom (start, duration, space),
                TweenType.To => transformToTween.RotateTo (end, duration, space),
                _ => default,
            };
        }

        public static ScaleTween GetScaleTweenType (
            TweenType tweenType, Transform transformToTween, Vector3 start, Vector3 end, float duration)
        {
            return tweenType switch
            {
                TweenType.FromTo => transformToTween.ScaleFromTo (start, end, duration),
                TweenType.From => transformToTween.ScaleFrom (start, duration),
                TweenType.To => transformToTween.ScaleTo (end, duration),
                _ => default,
            };
        }

        public static bool PositionIsCorrect (Transform transformToCheck, Vector3 targetPosition, Space space)
        {
            return space == Space.Self ?
                transformToCheck.localPosition == targetPosition :
                transformToCheck.position == targetPosition;
        }

        public static bool RotationIsCorrect (Transform transformToCheck, Vector3 targetPosition, Space space)
        {
            return space == Space.Self ?
                transformToCheck.localEulerAngles == targetPosition :
                transformToCheck.eulerAngles == targetPosition;
        }

        public static bool TweenWasMeantToFinishNow (double startTime, float duration)
        {
            return Math.Abs (Time.timeAsDouble - startTime) == duration;
        }
    }
}