using System.Collections.Generic;
using UnityEngine;

namespace JK.Tweening
{
    public class TweenManager : MonoBehaviour
    {
        private static TweenManager Instance;
        public static float CustomTimeScale { get; private set; } = 1f;
        private static readonly List<TweenBase> _tweens = new ();

        private void Awake ()
        {
            Debug.Assert (!Instance, $"More than one instance of {GetType ()}", Instance);
            Instance = this;
        }

        private void OnDestroy ()
        {
            _tweens.Clear ();
            CustomTimeScale = 1f;
            Instance = default;
        }

        private void Update ()
        {
            for (int i = 0; i < _tweens.Count; i++)
            {
                if (_tweens[i].IsPlaying)
                    _tweens[i].Update (Time.deltaTime * CustomTimeScale);
            }
        }

        public static void AddTween (TweenBase tween)
        {
            if (!_tweens.Contains (tween))
                _tweens.Add (tween);
        }

        public static void RemoveTween (TweenBase tween)
        {
            if (_tweens.Contains (tween))
                _tweens.Remove (tween);
        }

        public static void SetCustomTimeScale (float timeScale)
        {
            CustomTimeScale = timeScale;
        }

        public static void ResetCustomTimescale ()
        {
            CustomTimeScale = 1f;
        }
    }
}