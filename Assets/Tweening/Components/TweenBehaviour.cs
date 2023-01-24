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
                        transform.SetPosition (OriginalVector, TweeningSpace);
                        break;
                    case TweenClass.Rotate:
                        transform.SetRotation (OriginalVector, TweeningSpace);
                        break;
                    case TweenClass.Scale:
                        transform.localScale = OriginalVector;
                        break;
                    case TweenClass.Jump:
                        transform.SetPosition (OriginalVector, TweeningSpace);
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
                    return TargetTransform.MoveFromTo (StartPosition (), EndPosition (), Duration);
                case TweenClass.Rotate:
                    return TargetTransform.RotateFromTo (StartPosition (), EndPosition (), Duration);
                case TweenClass.Scale:
                    return TargetTransform.ScaleFromTo (StartPosition (), EndPosition (), Duration);
                case TweenClass.Jump:
                    return TargetTransform.JumpFromTo (StartPosition (), ArcTopPosition (), EndPosition (), Duration);
                case TweenClass.PunchPosition:
                    return TargetTransform.PunchPosition (EndPosition (), Duration, TweeningSpace);
                case TweenClass.PunchRotation:
                    return TargetTransform.PunchRotation (EndPosition (), Duration, TweeningSpace);
                case TweenClass.PunchScale:
                    return TargetTransform.PunchScale (EndPosition (), Duration);
                default:
                    return null;
            }
        }

        private Vector3 StartPosition ()
        {
            switch (TweenType)
            {
                case TweenType.FromTo:
                    return TweeningSpace.Matches (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector);
                case TweenType.From:
                    return TweeningSpace.Matches (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector);
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
                    return TweeningSpace.Matches (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector);
                case TweenType.From:
                    return TargetTransform.GetPosition (TweeningSpace);
                case TweenType.To:
                    return TweeningSpace.Matches (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector);
                default:
                    return Vector3.zero;
            }
        }

        private Vector3 ArcTopPosition ()
        {
            return TweeningSpace.Matches (Space.World) ? m_arcPeakPosition : TargetTransform.InverseTransformPoint (m_arcPeakPosition);
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
                case TweenClass.PunchPosition:
                    return TargetTransform.GetPosition (TweeningSpace);
                case TweenClass.PunchRotation:
                    return TargetTransform.GetRotation (TweeningSpace);
                case TweenClass.PunchScale:
                    return TargetTransform.localPosition;
                default:
                    return Vector3.zero;
            }
        }
    }
}