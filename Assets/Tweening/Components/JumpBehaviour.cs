using UnityEngine;

namespace JK.Tweening
{
    public class JumpBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private Vector3 m_startPosition;
        [SerializeField] private Vector3 m_endPosition;
        [SerializeField] private Vector3 m_upDirection;
        [SerializeField] private float m_height;

        public override void Play ()
        {
            ActiveTween = TargetTransform.JumpFromTo (
                m_startPosition, m_endPosition, m_height, Duration, m_upDirection, TweeningSpace);

            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
                transform.position = m_startPosition;

            base.Stop ();
        }
    }
}