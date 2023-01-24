using UnityEngine;

namespace JK.Tweening
{
    public class JumpTween : TweenBase
    {
        protected Transform _transform;
        protected Vector3 _originalPosition;
        protected Vector3 _startPosition;
        protected Vector3 _peakPosition;
        protected Vector3 _endPosition;
        protected Space _space;

        public JumpTween (Transform transform, Vector3 start, Vector3 peakPosition, Vector3 end, float duration, Space space = Space.Self)
        {
            _transform = transform;
            _originalPosition = start;
            _startPosition = start;
            _peakPosition = peakPosition;
            _endPosition = end;
            _duration = duration;
            _space = space;
        }

        public override void Reset ()
        {
            _transform.SetPosition (_originalPosition, _space);
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out bool completedLoop))
            {
                var newPosition = TweenUtility.QuadraticPosition (
                    _normalizedProgress, _startPosition, _peakPosition, _endPosition);

                _transform.SetPosition (newPosition, _space);

                if (completedLoop && _loopType.Matches (LoopType.Additive))
                {
                    var offset = _endPosition - _startPosition;
                    _startPosition = _endPosition;
                    _peakPosition += offset;
                    _endPosition += offset;
                }
            }
        }
    }
}