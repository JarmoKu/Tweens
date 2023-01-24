using System.Collections.Generic;
using UnityEngine;

namespace JK.Tweening
{
    public class TweenManager : MonoBehaviour
    {
        public static float CustomTimeScale { get; private set; } = 1f;

        private static TweenManager Instance;
        private static readonly List<IProgressable> _tweens = new ();
        private static int LatestFrameCount;

        private void Update ()
        {
            if (UpdateHasBeenCalledThisFrame ())
                return;

            for (int i = 0; i < _tweens.Count; i++)
            {
                if (_tweens[i].IsPlaying)
                    _tweens[i].Update (Time.deltaTime * CustomTimeScale);
            }
        }

        public static void AddTween (IProgressable tween)
        {
            if (Instance == default && Application.isPlaying)
                CreateInstance ();

            if (!_tweens.Contains (tween))
                _tweens.Add (tween);
        }

        public static void RemoveTween (IProgressable tween)
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

        private static void CreateInstance ()
        {
            Instance = new GameObject ("Tween Manager").AddComponent<TweenManager> ();
            Instance.enabled = true;

            DontDestroyOnLoad (Instance);
        }

        private bool UpdateHasBeenCalledThisFrame ()
        {
            if (LatestFrameCount == Time.frameCount)
            {
                Debug.LogWarning ("Update is being called more than once per frame. " +
                    "Make sure there is only one TweenManager instance");

                return true;
            }

            LatestFrameCount = Time.frameCount;
            return false;
        }
    }
}