using UnityEngine;
using UnityEngine.Events;

namespace JK.Tweening
{
    public class TransformTweenBehaviour : TweenBehaviourBase
    {
        #region PropertyNames for custom editor
#if UNITY_EDITOR
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
#endif
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
            m_onCompleted.Invoke ();
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
            return m_tweenClass switch
            {
                TweenClass.Move => TargetTransform.MoveFromTo (StartPosition (), EndPosition (), Duration),
                TweenClass.Rotate => TargetTransform.RotateFromTo (StartPosition (), EndPosition (), Duration),
                TweenClass.Scale => TargetTransform.ScaleFromTo (StartPosition (), EndPosition (), Duration),
                TweenClass.Jump => TargetTransform.JumpFromTo (StartPosition (), ArcTopPosition (), EndPosition (), Duration),
                TweenClass.PunchPosition => TargetTransform.PunchPosition (EndPosition (), Duration, TweeningSpace),
                TweenClass.PunchRotation => TargetTransform.PunchRotation (EndPosition (), Duration, TweeningSpace),
                TweenClass.PunchScale => TargetTransform.PunchScale (EndPosition (), Duration),
                _ => null,
            };
        }

        private Vector3 StartPosition ()
        {
            return TweenType switch
            {
                TweenType.FromTo => TweeningSpace.Matches (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector),
                TweenType.From => TweeningSpace.Matches (Space.World) ? StartVector : TargetTransform.InverseTransformPoint (StartVector),
                TweenType.To => TargetTransform.GetPosition (TweeningSpace),
                _ => Vector3.zero,
            };
        }

        private Vector3 EndPosition ()
        {
            return TweenType switch
            {
                TweenType.FromTo => TweeningSpace.Matches (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector),
                TweenType.From => TargetTransform.GetPosition (TweeningSpace),
                TweenType.To => TweeningSpace.Matches (Space.World) ? EndVector : TargetTransform.InverseTransformPoint (EndVector),
                _ => Vector3.zero,
            };
        }

        private Vector3 ArcTopPosition ()
        {
            return TweeningSpace.Matches (Space.World) ? m_arcPeakPosition : TargetTransform.InverseTransformPoint (m_arcPeakPosition);
        }

        private Vector3 GetOriginalPosition ()
        {
            return m_tweenClass switch
            {
                TweenClass.Move => TargetTransform.GetPosition (TweeningSpace),
                TweenClass.Rotate => TargetTransform.GetRotation (TweeningSpace),
                TweenClass.Scale => TargetTransform.localPosition,
                TweenClass.Jump => TargetTransform.GetPosition (TweeningSpace),
                TweenClass.PunchPosition => TargetTransform.GetPosition (TweeningSpace),
                TweenClass.PunchRotation => TargetTransform.GetRotation (TweeningSpace),
                TweenClass.PunchScale => TargetTransform.localPosition,
                _ => Vector3.zero,
            };
        }
    }
}