using UnityEngine;

namespace JK.Tweening
{
    public class SharedMaterialColorTween : TweenBase
    {
        protected Material _material;
        protected Gradient _gradient;
        protected Color _originalColor;
        protected Color _start;
        protected Color _end;
        protected bool _useGradient;

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