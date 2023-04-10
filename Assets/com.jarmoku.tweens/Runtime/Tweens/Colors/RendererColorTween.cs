using UnityEngine;

namespace JK.Tweening
{
    public class RendererColorTween : TweenBase
    {
        private readonly Color _originalColor;
        private readonly Material[] _materials;

        private Gradient _gradient;
        private bool _useGradient;
        private int _materialIndex;
        private Color _start;
        private Color _end;

        public RendererColorTween (Renderer image, int materialIndex, Color start, Color end, float duration)
        {
            _materials = image.materials;
            _originalColor = _materials[materialIndex].color;
            _start = start;
            _end = end;
            _duration = duration;
            _useGradient = false;
            _materialIndex = materialIndex;
        }

        public RendererColorTween (Renderer image, int materialIndex, Gradient gradient, float duration)
        {
            _materials = image.materials;
            _originalColor = _materials[materialIndex].color;
            _gradient = gradient;
            _duration = duration;
            _useGradient = true;
            _materialIndex = materialIndex;
        }

        public void SetColors (Color start, Color end, int materialIndex)
        {
            _start = start;
            _end = end;
            _useGradient = false;
            _materialIndex = materialIndex;
        }

        public void SetGradient (Gradient gradient, int materialIndex)
        {
            _gradient = gradient;
            _useGradient = true;
            _materialIndex = materialIndex;
        }

        public override void Reset ()
        {
            _materials[_materialIndex].color = _originalColor;
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out bool completedLoop))
            {
                _materials[_materialIndex].color = 
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