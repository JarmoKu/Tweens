using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    public class ImageColorTween : TweenBase
    {
        protected Image _image;
        protected Gradient _gradient;
        protected Color _originalColor;
        protected Color _start;
        protected Color _end;
        protected bool _useGradient;

        public ImageColorTween (Image image, Color start, Color end, float duration)
        {
            _image = image;
            _originalColor = image.color;
            _start = start;
            _end = end;
            _duration = duration;
            _useGradient = false;
        }

        public ImageColorTween (Image image, Gradient gradient, float duration)
        {
            _image = image;
            _originalColor = image.color;
            _gradient = gradient;
            _duration = duration;
            _useGradient = true;

            TweenManager.AddTween (this);
        }

        public override void Reset ()
        {
            _image.color = _originalColor;
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            // For some reason _normalizedProgress is NaN on first frame with this tween only.
            if (TryProgress (deltaTime, out bool completedLoop) && !float.IsNaN (_normalizedProgress))
            {
                _image.color = _useGradient ? _gradient.Evaluate (_normalizedProgress) : Color.Lerp (_start, _end, _normalizedProgress);

                if (completedLoop && _loopType.Equals (LoopType.Additive))
                {
                    _normalizedProgress = 0f;
                    var offset = _end - _start;
                    _start = _end;
                    _end += offset;
                }
            }
        }
    }
}