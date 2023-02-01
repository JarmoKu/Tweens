using UnityEngine;

namespace JK.Tweening
{
    public class JumpTween : TweenBase
    {
        private readonly Transform _transform;
        private readonly Vector3 _originalPosition;

        private Vector3 _startPosition;
        private Vector3 _peakPosition;
        private Vector3 _endPosition;
        private Space _space;

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

        public void SetTargets (Vector3 start, Vector3 peak, Vector3 end, Space space)
        {
            _startPosition = start;
            _peakPosition = peak;
            _endPosition = end;
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