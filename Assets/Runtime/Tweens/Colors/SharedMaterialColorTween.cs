using UnityEngine;

namespace JK.Tweening
{
    public class SharedMaterialColorTween : TweenBase
    {
        private readonly Material _material;
        private Gradient _gradient;
        private bool _useGradient;
        private readonly Color _originalColor;

        private Color _start;
        private Color _end;

        public SharedMaterialColorTween (Material material, Color start, Color end, float duration)
        {
            _material = material;
            _originalColor = material.color;
            _start = start;
            _end = end;
            _duration = duration;
            _useGradient = false;
        }

        public SharedMaterialColorTween (Material material, Gradient gradient, float duration)
        {
            _material = material;
            _originalColor = material.color;
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
            _material.color = _originalColor;
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out bool completedLoop))
            {
                _material.color = _useGradient ? _gradient.Evaluate (_normalizedProgress) : Color.Lerp (_start, _end, _normalizedProgress);

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