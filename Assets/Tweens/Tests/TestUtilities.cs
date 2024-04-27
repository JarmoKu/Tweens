using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

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

        public static JumpTween GetJumpTweenType (
            TweenType tweenType, Transform transformToTween, Vector3 start, Vector3 peak, Vector3 end, float duration, Space space)
        {
            return tweenType switch
            {
                TweenType.FromTo => transformToTween.JumpFromTo (start, peak, end, duration, space),
                TweenType.From => transformToTween.JumpFrom (start, peak, duration, space),
                TweenType.To => transformToTween.JumpTo (peak, end, duration, space),
                _ => default,
            };
        }

        public static JumpTween GetJumpTweenType (
            TweenType tweenType, Transform transformToTween, Vector3 start, float height, Vector3 end, float duration, Vector3 up, Space space)
        {
            return tweenType switch
            {
                TweenType.FromTo => transformToTween.JumpFromTo (start, end, height, duration, up, space),
                TweenType.From => transformToTween.JumpFrom (start, height, duration, up, space),
                TweenType.To => transformToTween.JumpTo (end, height, duration, up, space),
                _ => default,
            };
        }

        public static ImageColorTween GetImageColorTween (
            Image targetImage, ImageTweenType tweenType, Color start, Color end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetImage.ColorFromTo (start, end, duration),
                ImageTweenType.From => targetImage.ColorFrom (start, duration),
                ImageTweenType.To => targetImage.ColorTo (end, duration),
                _ => default,
            };
        }

        public static ImageColorTween GetImageAlphaTween (
            Image targetImage, ImageTweenType tweenType, float start, float end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetImage.AlphaFromTo (start, end, duration),
                ImageTweenType.From => targetImage.AlphaFrom (start, duration),
                ImageTweenType.To => targetImage.AlphaTo (end, duration),
                _ => default,
            };
        }

        public static SharedMaterialColorTween GetSharedMaterialColorTween (
            Material targetMaterial, ImageTweenType tweenType, Color start, Color end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetMaterial.ColorFromTo (start, end, duration),
                ImageTweenType.From => targetMaterial.ColorFrom (start, duration),
                ImageTweenType.To => targetMaterial.ColorTo (end, duration),
                _ => default,
            };
        }

        public static SharedMaterialColorTween GetSharedMaterialAlphaTween (
            Material targetMaterial, ImageTweenType tweenType, float start, float end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetMaterial.AlphaFromTo (start, end, duration),
                ImageTweenType.From => targetMaterial.AlphaFrom (start, duration),
                ImageTweenType.To => targetMaterial.AlphaTo (end, duration),
                _ => default,
            };
        }

        public static RendererColorTween GetRendererColorTween (
            Renderer targetRenderer, ImageTweenType tweenType, int materialIndex, Color start, Color end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetRenderer.ColorFromTo (materialIndex, start, end, duration),
                ImageTweenType.From => targetRenderer.ColorFrom (materialIndex, start, duration),
                ImageTweenType.To => targetRenderer.ColorTo (materialIndex, end, duration),
                _ => default,
            };
        }

        public static RendererColorTween GetRendererAlphaTween (
            Renderer targetRenderer, ImageTweenType tweenType, int materialIndex, float start, float end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetRenderer.AlphaFromTo (materialIndex, start, end, duration),
                ImageTweenType.From => targetRenderer.AlphaFrom (materialIndex, start, duration),
                ImageTweenType.To => targetRenderer.AlphaTo (materialIndex, end, duration),
                _ => default,
            };
        }

        public static SpriteColorTween GetSpriteColorTween (
            SpriteRenderer targetSprite, ImageTweenType tweenType, Color start, Color end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetSprite.ColorFromTo (start, end, duration),
                ImageTweenType.From => targetSprite.ColorFrom (start, duration),
                ImageTweenType.To => targetSprite.ColorTo (end, duration),
                _ => default,
            };
        }

        public static SpriteColorTween GetSpriteAlphaTween (
            SpriteRenderer targetSprite, ImageTweenType tweenType, float start, float end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetSprite.AlphaFromTo (start, end, duration),
                ImageTweenType.From => targetSprite.AlphaFrom (start, duration),
                ImageTweenType.To => targetSprite.AlphaTo (end, duration),
                _ => default,
            };
        }

        public static LightColorTween GetLightColorTween (
            Light targetLight, ImageTweenType tweenType, Color start, Color end, float duration)
        {
            return tweenType switch
            {
                ImageTweenType.FromTo => targetLight.ColorFromTo (start, end, duration),
                ImageTweenType.From => targetLight.ColorFrom (start, duration),
                ImageTweenType.To => targetLight.ColorTo (end, duration),
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