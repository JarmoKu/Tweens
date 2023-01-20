using UnityEngine;

namespace JK.Tweening
{
    public class PunchPositionTween : TweenBase
    {
        protected Transform _transform;
        protected Vector3 _startPosition;
        protected Vector3 _targetPosition;
        protected Space _space;

        public PunchPositionTween (Transform transform, Vector3 targetPosition, float duration, Space space)
        {
            _transform = transform;
            _startPosition = transform.GetPosition (space);
            _targetPosition = targetPosition;
            _duration = duration;
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
            _loopType = type.Equals (LoopType.None) ? type : LoopType.Repeat;
        }
    }
}