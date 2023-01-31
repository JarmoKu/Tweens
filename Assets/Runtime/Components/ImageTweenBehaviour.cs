using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    public class ImageTweenBehaviour : TweenBehaviourBase
    {
        #region PropertyNames for custom editor
#if UNITY_EDITOR
        public static string TargetSelfPropertyName => nameof (m_targetSelf);
        public static string LoopTypePropertyName => nameof (m_loopType);
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

        public override void Stop ()
        {
            m_attachedImage.color = OriginalColor;
            base.Stop ();
        }

        public override void Restart ()
        {
            if (ActiveTween == default)
                Play ();
            else
                base.Restart ();
        }

        public override void Play ()
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

            base.Play ();
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
                m_attachedImage = GetComponent<Image> ();
        }
    }
}