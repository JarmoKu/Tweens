using UnityEngine;

namespace JK.Tweening
{
    public abstract class TransformTweenBehaviourBase : MonoBehaviour
    {

        #region PropertyNames for custom editor
        public static string TargetSelfPropertyName => nameof (m_targetSelf);
        public static string TargetTransformPropertyName => nameof (m_targetTransform);
        public static string LoopCountPropertyName => nameof (m_loopCount);
        public static string LoopTypePropertyName => nameof (m_loopType);
        public static string LoopDelayPropertyName => nameof (m_loopDelay);
        #endregion

        [SerializeField] private bool m_targetSelf = true;
        [SerializeField] private Transform m_targetTransform;
        [Space]

        [SerializeField] private PlayOn m_playOn;
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

        protected TweenBase ActiveTween;
        protected float Duration => m_duration;
        protected Space TweeningSpace => m_space;
        protected Transform TargetTransform => m_targetSelf ? transform : m_targetTransform;

        private void Start ()
        {
            if (m_playOn.Equals (PlayOn.Start))
                Play ();
        }

        private void OnEnable ()
        {
            if (m_playOn.Equals (PlayOn.OnEnable))
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
                    TweenPreviewUpdater.StopPreview ();
#endif
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
                TweenPreviewUpdater.StartPreviev (ActiveTween);
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