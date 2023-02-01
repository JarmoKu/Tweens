using UnityEngine;

namespace JK.Tweening
{
    public class RotateTween : TweenBase
    {
        private readonly Transform _transform;
        private readonly Vector3 _originalRotation;
        private Vector3 _startRotation;
        private Vector3 _endRotation;
        private Space _space;

        public RotateTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            _transform = transform;
            _originalRotation = start;
            _startRotation = start;
            _endRotation = end;
            _duration = duration;
            _space = space;
        }

        public void SetTargets (Vector3 start, Vector3 end, Space space)
        {
            _startRotation = start;
            _endRotation = end;
            _space = space;
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var newRotation = _loopType.Matches (LoopType.Additive) ?
                    Vector3.LerpUnclamped (_startRotation, _endRotation, _totalProgress / _duration) :
                    Vector3.Lerp (_startRotation, _endRotation, _normalizedProgress);

                _transform.SetRotation (newRotation, _space);
            }
        }

        public override void Reset ()
        {
            _transform.SetRotation (_originalRotation, _space);
            BaseReset ();
        }
    }
}