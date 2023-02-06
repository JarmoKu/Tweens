using System;
using UnityEngine;

namespace JK.Tweening
{
    public abstract class TweenBehaviourBase : MonoBehaviour
    {
        public static event Action<TweenBase> StartedTween;
        public static event Action StoppedTween;

        protected TweenBase ActiveTween;

        public virtual void Play ()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                StartedTween?.Invoke (ActiveTween);
                return;
            }
#endif
            ActiveTween.Play ();
        }

        public virtual void Stop ()
        {
            if (ActiveTween != null)
            {
                ActiveTween.Pause ();
                ActiveTween.Reset ();

#if UNITY_EDITOR
                if (!Application.isPlaying)
                    StoppedTween?.Invoke ();
#endif
            }
        }

        public virtual void Restart ()
        {
            ActiveTween.Restart ();
        }
    }
}