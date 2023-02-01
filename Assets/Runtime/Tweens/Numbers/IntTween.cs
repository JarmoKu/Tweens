using System;
using UnityEngine;

namespace JK.Tweening
{
    public class IntTween : TweenBase
    {
        private readonly Action<int> _callback;
        private int _intervalProgress;

        private int _startValue;
        private int _endValue;
        private int _interval;

        public IntTween (int startingValue, int endValue, float duration, Action<int> callback)
        {
            _startValue = startingValue;
            _endValue = endValue;
            _callback = callback;
            _duration = duration;
        }

        public IntTween (int startingValue, int endValue, float duration, int increment, Action<int> callback)
        {
            _startValue = startingValue;
            _endValue = endValue;
            _callback = callback;
            _duration = duration;
            _interval = increment;
        }

        public void SetTargets (int start, int end, int interval)
        {
            _startValue = start;
            _endValue = end;
            _interval = interval;
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

                if (Mathf.Abs (value - _startValue) > _intervalProgress + _interval)
                {
                    _intervalProgress += _interval;

                    var finalValue = _startValue <= _endValue ? Mathf.FloorToInt (value) : Mathf.CeilToInt (value);
                    _callback?.Invoke (finalValue);
                }
            }
        }
    }
}