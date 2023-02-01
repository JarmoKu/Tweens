using UnityEngine;
using UnityEngine.UI;

namespace JK.Tweening
{
    public class ColorBehaviour : TweenBehaviourBase
    {
        #region PropertyNames for custom editor
#if UNITY_EDITOR
        public static string ColorTargetPropertyName => nameof (m_colorTarget);
        public static string TargetSelfPropertyName => nameof (m_targetSelf);
        public static string LoopTypePropertyName => nameof (m_loopType);
        public static string TargetImagePropertyName => nameof (m_targetImage);
        public static string TargetRendererPropertyName => nameof (m_targetRenderer);
        public static string TargetMaterialPropertyName => nameof (m_targetMaterial);
        public static string MaterialIndexPropertyName => nameof (m_materialIndex);
        public static string LoopCountPropertyName => nameof (m_loopCount);
        public static string LoopDelayPropertyName => nameof (m_loopDelay);
        public static string TweenTypePropertyName => nameof (m_tweenType);
        public static string StartPropertyName => nameof (m_startColor);
        public static string EndPropertyName => nameof (m_endColor);
        public static string GradientPropertyName => nameof (m_gradient);
#endif
        #endregion

        [SerializeField] private ColorTarget m_colorTarget;
        [SerializeField] private bool m_targetSelf;
        [SerializeField] private Image m_targetImage;
        [SerializeField] private Renderer m_targetRenderer;
        [SerializeField] private Material m_targetMaterial;
        [SerializeField] private int m_materialIndex;
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

        public void SetTargetSelf (bool targetSelf) => m_targetSelf = targetSelf;

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
            ResetColor ();
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
            ResetColor ();
            ActiveTween = ColorTween ();

            ActiveTween.SetLoops (m_loopCount, m_loopType);
            ActiveTween.SetLoopDelay (m_loopDelay);
            ActiveTween.SetEase (m_easeType);

            base.Play ();
        }

        private TweenBase ColorTween ()
        {
            return m_colorTarget switch
            {
                ColorTarget.Image => ImageTween (),
                ColorTarget.Material => MaterialTween (),
                ColorTarget.Renderer => RendererTween (),
                _ => default,
            };
        }

        private RendererColorTween RendererTween ()
        {
            return m_tweenType switch
            {
                ImageTweenType.FromTo => m_targetRenderer.ColorFromTo (m_materialIndex, m_startColor, m_endColor, m_duration),
                ImageTweenType.From => m_targetRenderer.ColorFrom (m_materialIndex, m_startColor, m_duration),
                ImageTweenType.To => m_targetRenderer.ColorTo (m_materialIndex, m_endColor, m_duration),
                ImageTweenType.Gradient => m_targetRenderer.ColorThroughGradient (m_materialIndex, m_gradient, m_duration),
                _ => default,
            };
        }

        private SharedMaterialColorTween MaterialTween ()
        {
            return m_tweenType switch
            {
                ImageTweenType.FromTo => m_targetMaterial.ColorFromTo (m_startColor, m_endColor, m_duration),
                ImageTweenType.From => m_targetMaterial.ColorFrom (m_startColor, m_duration),
                ImageTweenType.To => m_targetMaterial.ColorTo (m_endColor, m_duration),
                ImageTweenType.Gradient => m_targetMaterial.ColorThroughGradient (m_gradient, m_duration),
                _ => default,
            };
        }

        private ImageColorTween ImageTween ()
        {
            return m_tweenType switch
            {
                ImageTweenType.FromTo => m_targetImage.ColorFromTo (m_startColor, m_endColor, m_duration),
                ImageTweenType.From => m_targetImage.ColorFrom (m_startColor, m_duration),
                ImageTweenType.To => m_targetImage.ColorTo (m_endColor, m_duration),
                ImageTweenType.Gradient => m_targetImage.ColorThroughGradient (m_gradient, m_duration),
                _ => default,
            };
        }

        private Color CurrentColor ()
        {
            return m_colorTarget switch
            {
                ColorTarget.Image => m_targetImage.color,
                ColorTarget.Material => m_targetMaterial.color,
                ColorTarget.Renderer => m_targetRenderer.materials[m_materialIndex].color,
                _ => new Color (1f, 0.4f, 0.7f, 1f),
            };
        }

        private void ResetColor ()
        {
            switch (m_colorTarget)
            {
                case ColorTarget.Image:
                    m_targetImage.color = CurrentColor ();
                    break;
                case ColorTarget.Material:
                    m_targetMaterial.color = CurrentColor ();
                    break;
                case ColorTarget.Renderer:
                    m_targetRenderer.materials[m_materialIndex].color = CurrentColor ();
                    break;
            }
        }

        private void OnValidate ()
        {
            if (m_targetSelf)
            {
                switch (m_colorTarget)
                {
                    case ColorTarget.Image:
                        m_targetRenderer = default;
                        m_targetImage = GetComponent<Image> ();
                        m_targetMaterial = default;
                        break;
                    case ColorTarget.Material:
                        m_targetRenderer = default;
                        m_targetImage = default;
                        m_targetMaterial = default;
                        break;
                    case ColorTarget.Renderer:
                        m_targetRenderer = GetComponent<Renderer> ();
                        m_targetImage = default;
                        m_targetMaterial = default;
                        break;
                }
            }
            else
            {
                switch (m_colorTarget)
                {
                    case ColorTarget.Image:
                        m_targetRenderer = default;
                        m_targetMaterial = default;
                        break;
                    case ColorTarget.Material:
                        m_targetRenderer = default;
                        m_targetImage = default;
                        break;
                    case ColorTarget.Renderer:
                        m_targetImage = default;
                        m_targetMaterial = default;
                        break;
                }
            }
        }
    }
}