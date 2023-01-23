using System;
using UnityEngine;
using UnityEngine.Events;

namespace JK.Tweening
{
    public class NumberBehaviour : MonoBehaviour
    {
        [Serializable]
        public class IntEvent : UnityEvent<int> { }

        [Serializable]
        public class FloatEvent : UnityEvent<float> { }

        #region PropertyNames for custom editor
#if UNITY_EDITOR
        public static string LoopTypePropertyName => nameof (m_loopType);
        public static string LoopCountPropertyName => nameof (m_loopCount);
        public static string LoopDelayPropertyName => nameof (m_loopDelay);
        public static string ValueTypePropertyName => nameof (m_valueType);
        public static string FloatEventPropertyName => nameof (m_onUpdatedFloat);
        public static string IntEventPropertyName => nameof (m_onUpdatedInt);
        public static string FloatStartPropertyName => nameof (m_startFloat);
        public static string FloatEndPropertyName => nameof (m_endFloat);
        public static string IntStartPropertyName => nameof (m_startInt);
        public static string IntEndPropertyName => nameof (m_endInt);
#endif
        #endregion

        [SerializeField] private NumberTweens m_valueType;
        [SerializeField] private PlayOn m_playOn;
        [SerializeField] private EaseType m_easeType;
        [Min (0f)]
        [SerializeField] private float m_duration;
        [Min (0f)]
        [SerializeField] private int m_increment;

        [Space]
        [SerializeField] private float m_startFloat;
        [SerializeField] private float m_endFloat;
        [Space]
        [SerializeField] private int m_startInt;
        [SerializeField] private int m_endInt;

        [Header ("Looping")]
        [SerializeField] private LoopType m_loopType;
        [Min (-1)]
        [Tooltip ("-1 for infinite loops")]
        [SerializeField] private int m_loopCount;
        [Min (0f)]
        [SerializeField] private float m_loopDelay;

        [Space]
        [Tooltip ("For use in editor. Select Editor and Runtime")]
        [SerializeField] private FloatEvent m_onUpdatedFloat;
        [Space]
        [Tooltip ("For use in editor. Select Editor and Runtime")]
        [SerializeField] private IntEvent m_onUpdatedInt;

        public float StartFloat (float value) => m_startFloat = value;
        public float EndFloat (float value) => m_endFloat = value;
        public int StartInt (int value) => m_startInt = value;
        public int EndInt (int value) => m_endInt = value;

        private TweenBase _activeTween;

        private void Start ()
        {
            if (m_playOn.Equals (PlayOn.Start) && Application.isPlaying)
                Play ();
        }

        private void OnEnable ()
        {
            if (m_playOn.Equals (PlayOn.OnEnable) && Application.isPlaying)
            {
                if (_activeTween != null)
                    Restart ();
                else
                    Play ();
            }
        }

        public void Play ()
        {
            // Maybe old tween should be recycled?
            _activeTween = GetTween ();
            _activeTween.SetLoops (m_loopCount, m_loopType);
            _activeTween.SetLoopDelay (m_loopDelay);
            _activeTween.SetEase (m_easeType);

#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                TweenPreviewUpdater.StartPreviev (_activeTween);
                return;
            }
#endif
            _activeTween.Play ();
        }

        public void Restart ()
        {
            if (_activeTween == null)
                Play ();
            else
                _activeTween.Restart ();
        }

        public virtual void Stop ()
        {
            if (_activeTween != null)
            {
                _activeTween.Pause ();
                _activeTween.Reset ();

                if (m_valueType.Equals (NumberTweens.Float))
                    m_onUpdatedFloat.Invoke (m_startFloat);
                else
                    m_onUpdatedInt.Invoke (m_startInt);

#if UNITY_EDITOR
                if (!Application.isPlaying)
                    TweenPreviewUpdater.StopPreview ();
#endif
            }
        }

        private TweenBase GetTween ()
        {
            switch (m_valueType)
            {
                case NumberTweens.Float:
                    return new FloatTween (m_startFloat, m_endFloat, m_duration, m_increment, (value) => { OnFloatChanged (value); });
                case NumberTweens.Int:
                    return new IntTween (m_startInt, m_endInt, m_duration, (value) => { OnIntChanged (value); });
                default:
                    return null;
            }
        }

        private void OnFloatChanged (float value)
        {
            m_onUpdatedFloat.Invoke (value);
        }

        private void OnIntChanged (int value)
        {
            m_onUpdatedInt.Invoke (value);
        }
    }
}