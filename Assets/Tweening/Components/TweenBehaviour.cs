using UnityEngine;

namespace JK.Tweening
{
    [ExecuteAlways]
    public class TweenBehaviour : MonoBehaviour
    {
        [SerializeField] private TweenClass m_tweenClass = default;
        [SerializeField] private TweenType m_tweenType = default;
        [SerializeField] private Space m_space = default;

        [SerializeField] private EaseType m_easeType = default;
        [SerializeField] private float m_duration = default;
        [SerializeField] private Vector3 m_startPosition = default;
        [SerializeField] private Vector3 m_arcPeakPosition = default;
        [SerializeField] private Vector3 m_endPosition = default;

        [SerializeField] private LoopType m_loopType = default;
        [SerializeField] private int m_loopCount = default;
        [SerializeField] private float m_loopDelay = 1f;

        private TweenBase tween;

        private void OnEnable ()
        {
            Play ();
        }

        public void Play ()
        {
            tween = GetTween ();

            tween.SetLoopDelay (m_loopDelay);
            tween.SetLoops (m_loopCount, m_loopType);
            tween.SetEase (m_easeType);

            tween.Play ();
        }

        public void Stop ()
        {

        }

        private TweenBase GetTween ()
        {
            switch (m_tweenClass)
            {
                case TweenClass.Move:
                    return new MoveTween (transform, StartPosition (), EndPosition (), m_duration);
                case TweenClass.Rotate:
                    return new RotateTween (transform, StartPosition (), EndPosition (), m_duration);
                case TweenClass.Scale:
                    return new ScaleTween (transform, StartPosition (), EndPosition (), m_duration);
                case TweenClass.Jump:
                    return new JumpTween (transform, StartPosition (), ArcTopPosition (), EndPosition (), m_duration);
                default:
                    return null;
            }
        }

        private Vector3 StartPosition ()
        {
            switch (m_tweenType)
            {
                case TweenType.FromTo:
                    return m_space.Equals (Space.World) ? m_startPosition : transform.InverseTransformPoint (m_startPosition);
                case TweenType.From:
                    return m_space.Equals (Space.World) ? m_startPosition : transform.InverseTransformPoint (m_startPosition);
                case TweenType.To:
                    return m_space.Equals (Space.World) ? transform.position : transform.localPosition;
                default:
                    return Vector3.zero;
            }
        }

        private Vector3 EndPosition ()
        {
            switch (m_tweenType)
            {
                case TweenType.FromTo:
                    return m_space.Equals (Space.World) ? m_endPosition : transform.InverseTransformPoint (m_endPosition);
                case TweenType.From:
                    return m_space.Equals (Space.World) ? transform.position : transform.localPosition;
                case TweenType.To:
                    return m_space.Equals (Space.World) ? m_endPosition : transform.InverseTransformPoint (m_endPosition);
                default:
                    return Vector3.zero;
            }
        }

        private Vector3 ArcTopPosition ()
        {
            return m_space.Equals (Space.World) ? m_arcPeakPosition : transform.InverseTransformPoint (m_arcPeakPosition);
        }
    }
}