using UnityEngine;

namespace JK.Tweening
{
    public class PunchScaleTween : TweenBase
    {
        protected Transform _transform;
        protected Vector3 _startScale;
        protected Vector3 _endScale;

        public PunchScaleTween (Transform transform, Vector3 endScale, float duration)
        {
            _transform = transform;
            _startScale = transform.localScale;
            _endScale = endScale;
            _duration = duration;

            TweenManager.AddTween (this);
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var changeToScale = Vector3.Lerp (
                    _startScale, _endScale, Mathf.PingPong (_normalizedProgress * 2f, 1f));

                _transform.localScale = changeToScale;
            }
        }

        public override void Reset ()
        {
            _transform.localScale = _startScale;
            BaseReset ();
        }

        public override void SetLoops (int count, LoopType type = LoopType.None)
        {
            _loopCount = count;
            _loopType = type.Equals (LoopType.None) ? type : LoopType.Repeat;
        }
    }
}