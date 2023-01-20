using UnityEngine;

namespace JK.Tweening
{
    public class MoveBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private Vector3 m_startPosition;
        [SerializeField] private Vector3 m_endPosition;

        public override void Play ()
        {
            ActiveTween = TargetTransform.MoveFromTo (m_startPosition, m_endPosition, Duration, TweeningSpace);
            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
            {
                transform.position = m_startPosition;
            }

            base.Stop ();
        }
    }
}