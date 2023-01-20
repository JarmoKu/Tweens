using UnityEngine;

namespace JK.Tweening
{
    public class ImageColorBehaviour : ImageTweenBehaviour
    {
        [Space]
        [SerializeField] private Gradient m_gradient;

        public override void Play ()
        {
            ActiveTween = GetImage ().ColorThroughGradient (m_gradient, Duration);
            base.Play ();
        }
    }
}