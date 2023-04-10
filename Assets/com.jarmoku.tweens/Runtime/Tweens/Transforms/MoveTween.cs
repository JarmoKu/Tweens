using UnityEngine;

namespace JK.Tweening
{
    public class MoveTween : TweenBase
    {
        private readonly Transform _transform;
        private readonly Vector3 _originalStart;
        private Vector3 _start;
        private Vector3 _end;
        private Space _space;

        public MoveTween (Transform transform, Vector3 start, Vector3 end, float duration, Space space = Space.Self)
        {
            _transform = transform;
            _originalStart = start;
            _start = start;
            _end = end;
            _duration = duration;
            _space = space;
        }

        public void SetTargets (Vector3 start, Vector3 end, Space space)
        {
            _start = start;
            _end = end;
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