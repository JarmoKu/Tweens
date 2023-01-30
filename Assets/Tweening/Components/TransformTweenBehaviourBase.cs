using UnityEngine;
using UnityEngine.Events;

namespace JK.Tweening
{
    public static class TweenEditorEvents
    {
        public static event System.Action<TweenBase> StartedTween;
        public static event System.Action StoppedTween;

        public static void Started (TweenBase tweenBase)
        {
            StartedTween?.Invoke (tweenBase);
        }

        public static void Stopped (TweenBase tweenBase)
        {
            StoppedTween?.Invoke ();
        }
    }

    public abstract class TransformTweenBehaviourBase : MonoBehaviour
    {
        #region PropertyNames for custom editor
#if UNITY_EDITOR
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
        [SerializeField] private UnityEvent m_onCompleted;

        public Vector3 StartVector { get => m_start; protected set => m_start = value; }
        public Vector3 EndVector { get => m_end; protected set => m_end = value; }
        public Vector3 OriginalVector { get; protected set; }
        protected TweenType TweenType => m_tweenType;
        protected TweenBase ActiveTween;
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

        public void Restart ()
        {
            if (ActiveTween == null)
                Play ();
            else
                ActiveTween.Restart ();
        }

        public virtual void Stop ()
        {
            if (ActiveTween != null)
            {
                ActiveTween.Pause ();
                ActiveTween.Reset ();

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    TweenEditorEvents.Stopped (ActiveTween);
                    //TweenPreviewUpdater.StopPreview ();
                }
#endif

                m_onCompleted.Invoke ();
            }
        }

        public virtual void Play ()
        {
            ActiveTween.SetLoops (m_loopCount, m_loopType);
            ActiveTween.SetLoopDelay (m_loopDelay);
            ActiveTween.SetEase (m_easeType);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                TweenEditorEvents.Started (ActiveTween);//
                //TweenPreviewUpdater.StartPreviev (ActiveTween);
                return;
            }
#endif
            ActiveTween.Play ();
        }

        private void OnValidate ()
        {
            if (m_targetSelf && m_targetTransform != default)
            {
                m_targetTransform = default;
            }
        }
    }
}