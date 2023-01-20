using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    [RequireComponent (typeof (Image))]
    public abstract class ImageTweenBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayOn m_playOn;
        [Space]

        [SerializeField] private EaseType m_easeType;
        [Min (0)]
        [SerializeField] private float m_duration;
        [SerializeField] private LoopType m_loopType;
        [SerializeField] private int m_loopCount;
        [SerializeField] private float m_loopDelay;

        [HideInInspector]
        [SerializeField] private Image m_attachedImage;

        protected TweenBase ActiveTween;

        protected float Duration => m_duration;

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

        public void Stop ()
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

            ActiveTween.Play ();

#if UNITY_EDITOR
            if (!Application.isPlaying)
                TweenPreviewUpdater.StartPreviev (ActiveTween);
#endif
        }

        protected Image GetImage ()
        {
            if (!m_attachedImage)
                m_attachedImage = transform.GetComponent<Image> ();

            return m_attachedImage;
        }

        private void OnValidate ()
        {
            if (!m_attachedImage)
                m_attachedImage = transform.GetComponent<Image> ();
        }
    }
}