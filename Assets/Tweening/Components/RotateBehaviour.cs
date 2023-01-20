using UnityEngine;

namespace JK.Tweening
{
    public class RotateBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private Vector3 m_startRotation;
        [SerializeField] private Vector3 m_endRotation;

        public override void Play ()
        {
            ActiveTween = TargetTransform.RotateFromTo (m_startRotation, m_endRotation, Duration, TweeningSpace);
            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
            {
                transform.position = m_startRotation;
            }

            base.Stop ();
        }
    }
}