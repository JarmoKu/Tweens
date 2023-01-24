using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    [RequireComponent (typeof (Image))]
    public class ImageTweenBehaviour : MonoBehaviour
    {
        #region PropertyNames for custom editor
#if UNITY_EDITOR
        public static string TargetImagePropertyName => nameof (m_attachedImage);
        public static string LoopCountPropertyName => nameof (m_loopCount);
        public static string LoopDelayPropertyName => nameof (m_loopDelay);
        public static string TweenTypePropertyName => nameof (m_tweenType);
        public static string StartPropertyName => nameof (m_startColor);
        public static string EndPropertyName => nameof (m_endColor);
        public static string GradientPropertyName => nameof (m_gradient);
#endif
        #endregion

        [SerializeField] private bool m_targetSelf;
        [SerializeField] private Image m_attachedImage;
        [Space]

        [SerializeField] private PlayOn m_playOn;
        [SerializeField] private ImageTweenType m_tweenType;
        [Space]

        [SerializeField] private EaseType m_easeType;
        [Min (0)]
        [SerializeField] private float m_duration;
        [SerializeField] private LoopType m_loopType;
        [SerializeField] private int m_loopCount;
        [SerializeField] private float m_loopDelay;

        [Space]
        [SerializeField] private Color m_startColor;
        [SerializeField] private Color m_endColor;

        [Space]
        [SerializeField] private Gradient m_gradient;

        private TweenBase ActiveTween;
        private Color OriginalColor;

        private void Start ()
        {
            if (m_playOn.Matches (PlayOn.Start))
                Play ();
        }

        private void OnEnable ()
        {
            if (m_playOn.Matches (PlayOn.OnEnable))
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
                m_attachedImage.color = OriginalColor;

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
            OriginalColor = m_attachedImage.color;

            switch (m_tweenType)
            {
                case ImageTweenType.FromTo:
                    ActiveTween = m_attachedImage.ColorFromTo (m_startColor, m_endColor, m_duration);
                    break;
                case ImageTweenType.From:
                    ActiveTween = m_attachedImage.ColorFrom (m_startColor, m_duration);
                    break;
                case ImageTweenType.To:
                    ActiveTween = m_attachedImage.ColorTo (m_endColor, m_duration);
                    break;
                case ImageTweenType.Gradient:
                    ActiveTween = m_attachedImage.ColorThroughGradient (m_gradient, m_duration);
                    break;
            }

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

        protected Image GetImage ()
        {
            if (!m_attachedImage)
                m_attachedImage = transform.GetComponent<Image> ();

            return m_attachedImage;
        }

        private void OnValidate ()
        {
            if (m_targetSelf)
            {
                m_attachedImage = GetComponent<Image> ();
            }
        }
    }
}