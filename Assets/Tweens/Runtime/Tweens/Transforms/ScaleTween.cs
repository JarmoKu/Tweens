using UnityEngine;

namespace JK.Tweening
{
    public class ScaleTween : TweenBase
    {
        private readonly Transform _transform;
        private readonly Vector3 _originalScale;
        private Vector3 _startScale;
        private Vector3 _endScale;

        public ScaleTween (Transform transform, Vector3 start, Vector3 end, float duration)
        {
            _transform = transform;
            _originalScale = start;
            _startScale = start;
            _endScale = end;
            _duration = duration;
        }

        public void SetTargets (Vector3 start, Vector3 end)
        {
            _startScale = start;
            _endScale = end;
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var newScale = _loopType.Matches (LoopType.Additive) ?
                    Vector3.LerpUnclamped (_startScale, _endScale, _totalProgress / _duration) :
                    Vector3.Lerp (_startScale, _endScale, _normalizedProgress);

                _transform.localScale = newScale;
            }
        }

        public override void Reset ()
        {
            _transform.localScale = _originalScale;
            BaseReset ();
        }
    }
}