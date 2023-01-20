using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    public static class ImageColorExtensions
    {
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
    }
}