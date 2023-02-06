using UnityEngine;

namespace JK.Tweening
{
    public class PunchRotationTween : TweenBase
    {
        private readonly Transform _transform;

        private Vector3 _startRotation;
        private Vector3 _targetRotation;
        private Space _space;

        public PunchRotationTween (Transform transform, Vector3 targetRotation, float duration, Space space)
        {
            _transform = transform;
            _startRotation = transform.GetRotation (space);
            _targetRotation = targetRotation;
            _duration = duration;
            _space = space;
        }

        public void SetTargets (Vector3 start, Vector3 end, Space space)
        {
            _startRotation = start;
            _targetRotation = end;
            _space = space;
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var changeToRotation = Vector3.Lerp (
                    _startRotation, _targetRotation, Mathf.PingPong (_normalizedProgress * 2f, 1f));

                _transform.SetRotation (changeToRotation, _space);
            }
        }

        public override void Reset ()
        {
            _transform.SetRotation (_startRotation, _space);
            BaseReset ();
        }

        public override void SetLoops (int count, LoopType type = LoopType.None)
        {
            _loopCount = count;
            _loopType = type.Matches (LoopType.None) ? type : LoopType.Repeat;
        }
    }
}