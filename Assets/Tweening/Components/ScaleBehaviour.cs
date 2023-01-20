using UnityEngine;

namespace JK.Tweening
{
    public class ScaleBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private Vector3 m_startScale;
        [SerializeField] private Vector3 m_endScale;

        public override void Play ()
        {
            ActiveTween = TargetTransform.ScaleFromTo (m_startScale, m_endScale, Duration);
            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
            {
                TargetTransform.localScale = m_endScale;
            }

            base.Stop ();
        }
    }
}