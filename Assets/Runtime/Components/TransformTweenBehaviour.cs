using UnityEngine;
using UnityEngine.Events;

namespace JK.Tweening
{
    public class TransformTweenBehaviour : TweenBehaviourBase
    {
        #region PropertyNames for custom editor
        public static string ArcPeakPropertyName => nameof (m_arcPeakPosition);
        public static string TweenClassPropertyName => nameof (m_tweenClass);
        public static string TargetSelfPropertyName => nameof (m_targetSelf);
        public static string TargetTransformPropertyName => nameof (m_targetTransform);
        public static string LoopCountPropertyName => nameof (m_loopCount);
        public static string LoopTypePropertyName => nameof (m_loopType);
        public static string LoopDelayPropertyName => nameof (m_loopDelay);
        public static string TweenTypePropertyName => nameof (m_tweenType);
        public static string StartPropertyName => nameof (m_start);
        public static string EndPropertyName => nameof (m_end);
        #endregion

        public void SetTweenClass (TweenClass tweenClass) => m_tweenClass = tweenClass;

        [SerializeField] private TweenClass m_tweenClass;
        [SerializeField] private bool m_targetSelf = true;
        [SerializeField] private Transform m_targetTransform;
        [Space]

        [SerializeField] private PlayOn m_playOn;
        [SerializeField] private TweenType m_tweenType;
        [Space]

        [SerializeField] private Space m_space;
        [SerializeField] private EaseType m_easeType;
        [Min (0f)]
        [SerializeField] private float m_duration;

        [Header ("Looping")]
        [SerializeField] private LoopType m_loopType;
        [Min (-1)]
        [Tooltip ("-1 for infinite loops")]
        [SerializeField] private int m_loopCount;
        [Min (0f)]
        [SerializeField] private float m_loopDelay;

        [Space]
        [SerializeField] private Vector3 m_start;
        [Space]
        [SerializeField] private Vector3 m_end;
        [Space]
        [SerializeField] private Vector3 m_arcPeakPosition;
        [Space]
        [SerializeField] private UnityEvent m_onCompleted;

        public Vector3 StartVector { get => m_start; protected set => m_start = value; }
        public Vector3 EndVector { get => m_end; protected set => m_end = value; }
        public Vector3 OriginalVector { get; protected set; }
        protected TweenType TweenType => m_tweenType;
        protected float Duration => m_duration;
        protected Space TweeningSpace => m_space;
        protected Transform TargetTransform => m_targetSelf ? transform : m_targetTransform;

        private void Start ()
        {
            if (m_playOn.Matches (PlayOn.Start) && Application.isPlaying)
                Play ();
        }

        private void OnEnable ()
        {
            if (m_playOn.Matches (PlayOn.OnEnable) && Application.isPlaying)
            {
                if (ActiveTween != null)
                    Restart ();
                else
                    Play ();
            }
        }

        public override void Play ()
        {
            OriginalVector = GetOriginalPosition ();
            ActiveTween = GetTween ();

            ActiveTween.SetLoops (m_loopCount, m_loopType);
            ActiveTween.SetLoopDelay (m_loopDelay);
            ActiveTween.SetEase (m_easeType);

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

        public override void Restart ()
        {
            if (ActiveTween == default)
                Play ();
            else
                base.Restart ();
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