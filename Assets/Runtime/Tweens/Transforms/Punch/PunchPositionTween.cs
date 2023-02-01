using UnityEngine;

namespace JK.Tweening
{
    public class PunchPositionTween : TweenBase
    {
        private readonly Transform _transform;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Space _space;

        public PunchPositionTween (Transform transform, Vector3 targetPosition, float duration, Space space)
        {
            _transform = transform;
            _startPosition = transform.GetPosition (space);
            _targetPosition = targetPosition;
            _duration = duration;
            _space = space;
        }

        public void SetTargets (Vector3 start, Vector3 end, Space space)
        {
            _startPosition = start;
            _targetPosition = end;
            _space = space;
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var changeToPosition = Vector3.Lerp (
                    _startPosition, _targetPosition, Mathf.PingPong (_normalizedProgress * 2f, 1f));

                _transform.SetPosition (changeToPosition, _space);
            }
        }

        public override void Reset ()
        {
            _transform.SetPosition (_startPosition, _space);
            BaseReset ();
        }

        public override void SetLoops (int count, LoopType type = LoopType.None)
        {
            _loopCount = count;
            _loopType = type.Matches (LoopType.None) ? type : LoopType.Repeat;
        }
    }
}