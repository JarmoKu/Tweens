using UnityEngine;

namespace JK.Tweening
{
    [ExecuteAlways]
    public class TweenBehaviour : TransformTweenBehaviourBase
    {
        #region PropertyNames for custom editor
        public static string ArcPeakPropertyName => nameof (m_arcPeakPosition);
        public static string TweenClassPropertyName => nameof (m_tweenClass);
        #endregion

        public void SetTweenClass (TweenClass tweenClass) => m_tweenClass = tweenClass;

        [SerializeField] private TweenClass m_tweenClass;
        [SerializeField] private Vector3 m_arcPeakPosition;

        public override void Play ()
        {
            OriginalVector = GetOriginalPosition ();
            ActiveTween = GetTween ();
            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
            {
                switch (m_tweenClass)
                {
                    case TweenClass.Move:
                        transform.SetPosition (StartVector, TweeningSpace);
                        break;
                    case TweenClass.Rotate:
                        transform.SetRotation (StartVector, TweeningSpace);
                        break;
                    case TweenClass.Scale:
                        transform.localScale = StartVector;
                        break;
                    case TweenClass.Jump:
                        transform.SetPosition (StartVector, TweeningSpace);
                        break;
                }
            }

            base.Stop ();
        }

        private TweenBase GetTween ()
        {
            switch (m_tweenClass)
            {
                case TweenClass.Move:
                    return new MoveTween (transform, StartPosition (), EndPosition (), Duration);
                case TweenClass.Rotate:
                    return new RotateTween (transform, StartPosition (), EndPosition (), Duration);
                case TweenClass.Scale:
                    return new ScaleTween (transform, StartPosition (), EndPosition (), Duration);
                case TweenClass.Jump:
                    return new JumpTween (transform, StartPosition (), ArcTopPosition (), EndPosition (), Duration);
                default:
                    return null;
            }
        }

        private Vector3 StartPosition ()
        {
            switch (TweenType)
            {
                case TweenType.FromTo:
                    return TweeningSpace.Equals (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector);
                case TweenType.From:
                    return TweeningSpace.Equals (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector);
                case TweenType.To:
                    return TargetTransform.GetPosition (TweeningSpace);
                default:
                    return Vector3.zero;
            }
        }

        private Vector3 EndPosition ()
        {
            switch (TweenType)
            {
                case TweenType.FromTo:
                    return TweeningSpace.Equals (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector);
                case TweenType.From:
                    return TargetTransform.GetPosition (TweeningSpace);
                case TweenType.To:
                    return TweeningSpace.Equals (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector);
                default:
                    return Vector3.zero;
            }
        }

        private Vector3 ArcTopPosition ()
        {
            return TweeningSpace.Equals (Space.World) ? m_arcPeakPosition : TargetTransform.InverseTransformPoint (m_arcPeakPosition);
        }

        private Vector3 GetOriginalPosition ()
        {
            switch (m_tweenClass)
            {
                case TweenClass.Move:
                    return TargetTransform.GetPosition (TweeningSpace);
                case TweenClass.Rotate:
                    return TargetTransform.GetRotation (TweeningSpace);
                case TweenClass.Scale:
                    return TargetTransform.localPosition;
                case TweenClass.Jump:
                    return TargetTransform.GetPosition (TweeningSpace);
                default:
                    return Vector3.zero;
            }
        }
    }
}