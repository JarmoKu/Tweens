using System;
using UnityEngine;
using UnityEngine.UI;

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
        public static PunchPositionTween PunchPosition (this Transform transform, Vector3 targetPosition, float duration, Space space = Space.World)
        {
            return new PunchPositionTween (transform, targetPosition, duration, space);
        }

        public static PunchRotationTween PunchRotation (this Transform transform, Vector3 targetRotation, float duration, Space space = Space.World)
        {
            return new PunchRotationTween (transform, targetRotation, duration, space);
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

        #region Image
        public static ImageColorTween ColorFromTo (this Image image, Color start, Color end, float duration)
        {
            return new ImageColorTween (image, start, end, duration);
        }

        public static ImageColorTween ColorFrom (this Image image, Color start, float duration)
        {
            return new ImageColorTween (image, start, image.color, duration);
        }

        public static ImageColorTween ColorTo (this Image image, Color end, float duration)
        {
            return new ImageColorTween (image, image.color, end, duration);
        }

        public static ImageColorTween ColorThroughGradient (this Image image, Gradient gradient, float duration)
        {
            return new ImageColorTween (image, gradient, duration);
        }

        public static ImageColorTween AlphaFromTo (this Image image, float start, float end, float duration)
        {
            var startColor = image.color;
            startColor.a = start;

            var endColor = image.color;
            endColor.a = end;

            return new ImageColorTween (image, startColor, endColor, duration);
        }

        public static ImageColorTween AlphaFrom (this Image image, float start, float duration)
        {
            var startColor = image.color;
            startColor.a = start;

            return new ImageColorTween (image, startColor, image.color, duration);
        }

        public static ImageColorTween AlphaTo (this Image image, float end, float duration)
        {
            var endColor = image.color;
            endColor.a = end;

            return new ImageColorTween (image, image.color, endColor, duration);
        }
        #endregion

        #region Material
        public static SharedMaterialColorTween ColorFromTo (this Material material, Color start, Color end, float duration)
        {
            return new SharedMaterialColorTween (material, start, end, duration);
        }

        public static SharedMaterialColorTween ColorFrom (this Material material, Color start, float duration)
        {
            return new SharedMaterialColorTween (material, start, material.color, duration);
        }

        public static SharedMaterialColorTween ColorTo (this Material material, Color end, float duration)
        {
            return new SharedMaterialColorTween (material, material.color, end, duration);
        }

        public static SharedMaterialColorTween ColorThroughGradient (this Material material, Gradient gradient, float duration)
        {
            return new SharedMaterialColorTween (material, gradient, duration);
        }

        public static SharedMaterialColorTween AlphaFromTo (this Material material, float start, float end, float duration)
        {
            var startColor = material.color;
            startColor.a = start;

            var endColor = material.color;
            endColor.a = end;

            return new SharedMaterialColorTween (material, startColor, endColor, duration);
        }

        public static SharedMaterialColorTween AlphaFrom (this Material material, float start, float duration)
        {
            var startColor = material.color;
            startColor.a = start;

            return new SharedMaterialColorTween (material, startColor, material.color, duration);
        }

        public static SharedMaterialColorTween AlphaTo (this Material material, float end, float duration)
        {
            var endColor = material.color;
            endColor.a = end;

            return new SharedMaterialColorTween (material, material.color, endColor, duration);
        }
        #endregion

        #region Renderer
        public static RendererColorTween ColorFromTo (this Renderer renderer, int materialIndex, Color start, Color end, float duration)
        {
            return new RendererColorTween (renderer, materialIndex, start, end, duration);
        }

        public static RendererColorTween ColorFrom (this Renderer renderer, int materialIndex, Color start, float duration)
        {
            return new RendererColorTween (renderer, materialIndex, start, renderer.materials[materialIndex].color, duration);
        }

        public static RendererColorTween ColorTo (this Renderer renderer, int materialIndex, Color end, float duration)
        {
            return new RendererColorTween (renderer, materialIndex, renderer.materials[materialIndex].color, end, duration);
        }

        public static RendererColorTween ColorThroughGradient (this Renderer renderer, int materialIndex, Gradient gradient, float duration)
        {
            return new RendererColorTween (renderer, materialIndex, gradient, duration);
        }

        public static RendererColorTween AlphaFromTo (this Renderer renderer, int materialIndex, float start, float end, float duration)
        {
            var originalColor = renderer.materials[materialIndex].color;
            var startColor = originalColor;
            startColor.a = start;

            var endColor = originalColor;
            endColor.a = end;

            return new RendererColorTween (renderer, materialIndex, startColor, endColor, duration);
        }

        public static RendererColorTween AlphaFrom (this Renderer renderer, int materialIndex, float start, float duration)
        {
            var originalColor = renderer.materials[materialIndex].color;
            var startColor = originalColor;
            startColor.a = start;

            return new RendererColorTween (renderer, materialIndex, startColor, originalColor, duration);
        }

        public static RendererColorTween AlphaTo (this Renderer renderer, int materialIndex, float end, float duration)
        {
            var originalColor = renderer.materials[materialIndex].color;
            var endColor = originalColor;
            endColor.a = end;

            return new RendererColorTween (renderer, materialIndex, originalColor, endColor, duration);
        }
        #endregion

        #region SpriteRenderer
        public static SpriteColorTween ColorFromTo (this SpriteRenderer renderer, Color start, Color end, float duration)
        {
            return new SpriteColorTween (renderer, start, end, duration);
        }

        public static SpriteColorTween ColorFrom (this SpriteRenderer renderer, Color start, float duration)
        {
            return new SpriteColorTween (renderer, start, renderer.color, duration);
        }

        public static SpriteColorTween ColorTo (this SpriteRenderer renderer, Color end, float duration)
        {
            return new SpriteColorTween (renderer, renderer.color, end, duration);
        }

        public static SpriteColorTween ColorThroughGradient (this SpriteRenderer renderer, Gradient gradient, float duration)
        {
            return new SpriteColorTween (renderer, gradient, duration);
        }

        public static SpriteColorTween AlphaFromTo (this SpriteRenderer renderer, float start, float end, float duration)
        {
            var originalColor = renderer.color;
            var startColor = originalColor;
            startColor.a = start;

            var endColor = originalColor;
            endColor.a = end;

            return new SpriteColorTween (renderer, startColor, endColor, duration);
        }

        public static SpriteColorTween AlphaFrom (this SpriteRenderer renderer, float start, float duration)
        {
            var originalColor = renderer.color;
            var startColor = originalColor;
            startColor.a = start;

            return new SpriteColorTween (renderer, startColor, originalColor, duration);
        }

        public static SpriteColorTween AlphaTo (this SpriteRenderer renderer, float end, float duration)
        {
            var originalColor = renderer.color;
            var endColor = originalColor;
            endColor.a = end;

            return new SpriteColorTween (renderer, originalColor, endColor, duration);
        }
        #endregion

        #region Light
        public static LightColorTween ColorFromTo (this Light light, Color start, Color end, float duration)
        {
            return new LightColorTween (light, start, end, duration);
        }

        public static LightColorTween ColorFrom (this Light light, Color start, float duration)
        {
            return new LightColorTween (light, start, light.color, duration);
        }

        public static LightColorTween ColorTo (this Light light, Color end, float duration)
        {
            return new LightColorTween (light, light.color, end, duration);
        }

        public static LightColorTween ColorThroughGradient (this Light light, Gradient gradient, float duration)
        {
            return new LightColorTween (light, gradient, duration);
        }
        #endregion

        #region Enums
        public static bool Matches (this LoopType comparison1, LoopType comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        public static bool Matches (this TweenClass comparison1, TweenClass comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        public static bool Matches (this TweenType comparison1, TweenType comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        public static bool Matches (this ColorTarget comparison1, ColorTarget comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this Space comparison1, Space comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this ImageTweenType comparison1, ImageTweenType comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this NumberTweens comparison1, NumberTweens comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this PlayOn comparison1, PlayOn comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this PunchType comparison1, PunchType comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }

        internal static bool Matches (this EaseType comparison1, EaseType comparison2)
        {
            return (int)comparison1 == (int)comparison2;
        }
        #endregion

        #region Transform
        internal static void SetPosition (this Transform transform, Vector3 position, Space space)
        {
            if (space.Matches (Space.Self))
                transform.localPosition = position;
            else
                transform.position = position;
        }

        internal static void SetRotation (this Transform transform, Vector3 rotation, Space space)
        {
            if (space.Matches (Space.Self))
                transform.localRotation = Quaternion.Euler (rotation);
            else
                transform.rotation = Quaternion.Euler (rotation);
        }

        internal static Vector3 GetPosition (this Transform transform, Space space)
        {
            return space.Matches (Space.World) ? transform.position : transform.localPosition;
        }

        internal static Vector3 GetRotation (this Transform transform, Space space)
        {
            return space.Matches (Space.World) ? transform.eulerAngles : transform.localEulerAngles;
        }
        #endregion
    }
}