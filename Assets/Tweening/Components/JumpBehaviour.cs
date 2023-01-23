using UnityEngine;

namespace JK.Tweening
{
    public class JumpBehaviour : TransformTweenBehaviourBase
    {
        [SerializeField] private Vector3 m_upDirection;
        [SerializeField] private float m_height;

        public override void Play ()
        {
            OriginalVector = TargetTransform.GetPosition (TweeningSpace);

            switch (base.TweenType)
            {
                case TweenType.FromTo:
                    ActiveTween = TargetTransform.JumpFromTo (
                        StartVector, EndVector, m_height, Duration, m_upDirection, TweeningSpace);
                    break;
                case TweenType.From:
                    ActiveTween = TargetTransform.JumpFrom (
                        StartVector, m_height, Duration, m_upDirection, TweeningSpace);
                    break;
                case TweenType.To:
                    ActiveTween = TargetTransform.JumpTo (
                        EndVector, m_height, Duration, m_upDirection, TweeningSpace);
                    break;
                default:
                    Debug.LogError ("No tween type found");
                    break;
            }

            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
                transform.position = OriginalVector;

            base.Stop ();
        }
    }
}