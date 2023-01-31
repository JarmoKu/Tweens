using UnityEngine;

namespace JK.Tweening
{
    public class RendererColorTween : TweenBase
    {
        protected Renderer _renderer;
        protected Gradient _gradient;
        protected Color _originalColor;
        protected Color _start;
        protected Color _end;
        protected bool _useGradient;
        protected int _materialIndex;

        public RendererColorTween (Renderer image, int materialIndex, Color start, Color end, float duration)
        {
            _renderer = image;
            _originalColor = image.materials[materialIndex].color;
            _start = start;
            _end = end;
            _duration = duration;
            _useGradient = false;
            _materialIndex = materialIndex;
        }

        public RendererColorTween (Renderer image, int materialIndex, Gradient gradient, float duration)
        {
            _renderer = image;
            _originalColor = image.materials[materialIndex].color;
            _gradient = gradient;
            _duration = duration;
            _useGradient = true;
            _materialIndex = materialIndex;
        }

        public override void Reset ()
        {
            _renderer.materials[_materialIndex].color = _originalColor;
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out bool completedLoop))
            {
                _renderer.materials[_materialIndex].color = 
                    _useGradient ? _gradient.Evaluate (_normalizedProgress) : Color.Lerp (_start, _end, _normalizedProgress);

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