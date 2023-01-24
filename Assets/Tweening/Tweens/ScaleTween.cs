using UnityEngine;

namespace JK.Tweening
{
    public class ScaleTween : TweenBase
    {
        protected Transform _transform;
        protected Vector3 _originalScale;
        protected Vector3 _startScale;
        protected Vector3 _endScale;

        public ScaleTween (Transform transform, Vector3 start, Vector3 end, float duration)
        {
            _transform = transform;
            _originalScale = start;
            _startScale = start;
            _endScale = end;
            _duration = duration;
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
            _transform.localScale = _startScale;
            BaseReset ();
        }
    }
}