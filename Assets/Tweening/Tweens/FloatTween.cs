using System;
using UnityEngine;

namespace JK.Tweening
{
    public class FloatTween : TweenBase
    {
        private readonly Action<float> _callback;
        private readonly float _startValue;
        private readonly float _endValue;
        private readonly float _interval;
        private float _intervalProgress;

        public FloatTween (float startingValue, float endValue, float duration)
        {
            _startValue = startingValue;
            _endValue = endValue;
            _duration = duration;
        }

        public FloatTween (float startingValue, float endValue, float duration, Action<float> callback)
        {
            _startValue = startingValue;
            _endValue = endValue;
            _callback = callback;
            _duration = duration;
        }

        public FloatTween (float startingValue, float endValue, float duration, float increment, Action<float> callback)
        {
            _startValue = startingValue;
            _endValue = endValue;
            _callback = callback;
            _duration = duration;
            _interval = increment;
        }

        public override void Reset ()
        {
            BaseReset ();
        }

        public override void Update (float deltaTime)
        {
            if (TryProgress (deltaTime, out _))
            {
                var value = Mathf.Lerp (_startValue, _endValue, _normalizedProgress);
                if (_interval == default)
                {
                    if (_callback != null)
                        _callback?.Invoke (value);
                }
                else
                {
                    var nearestMultipleOfInterval = Mathf.FloorToInt (value / _interval) * _interval;
                    if (nearestMultipleOfInterval != _intervalProgress)
                    {
                        _intervalProgress = nearestMultipleOfInterval;

                        if (_callback != null)
                            _callback?.Invoke (_intervalProgress);
                    }
                }
            }
        }
    }
}