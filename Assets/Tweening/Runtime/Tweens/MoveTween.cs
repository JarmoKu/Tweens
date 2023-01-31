using UnityEngine;

namespace JK.Tweening
{
    public class MoveTween : TweenBase
    {
        protected Transform _transform;
        protected Vector3 _originalStart;
        protected Vector3 _start;
        protected Vector3 _end;
        protected Space _space;

        public MoveTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            _transform = transform;
            _originalStart = start;
            _start = start;
            _end = end;
            _duration = duration;
            _space = space;
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var newPosition = _loopType.Matches (LoopType.Additive) ?
                    Vector3.LerpUnclamped (_start, _end, _totalProgress / _duration) :
                    Vector3.Lerp (_start, _end, _normalizedProgress);

                _transform.SetPosition (newPosition, _space);
            }
        }

        public override void Reset ()
        {
            _transform.SetPosition (_originalStart, _space);
            BaseReset ();
        }
    }
}