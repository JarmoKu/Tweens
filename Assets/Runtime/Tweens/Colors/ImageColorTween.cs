using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    public class ImageColorTween : TweenBase
    {
        private readonly Image _image;
        private readonly Color _originalColor;

        private bool _useGradient;
        private Gradient _gradient;
        private Color _start;
        private Color _end;

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
        }

        public void SetColors (Color start, Color end)
        {
            _start = start;
            _end = end;
            _useGradient = false;
        }

        public void SetGradient (Gradient gradient)
        {
            _gradient = gradient;
            _useGradient = true;
        }

        public override void Reset ()
        {
            _image.color = _originalColor;
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out bool completedLoop))
            {
                _image.color = _useGradient ? _gradient.Evaluate (_normalizedProgress) : Color.Lerp (_start, _end, _normalizedProgress);

                if (completedLoop && _loopType.Matches (LoopType.Additive))
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