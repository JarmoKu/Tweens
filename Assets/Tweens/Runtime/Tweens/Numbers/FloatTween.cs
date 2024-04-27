using System;
using UnityEngine;

namespace JK.Tweening
{
    public class FloatTween : TweenBase
    {
        private readonly Action<float> _callback;
        private float _intervalProgress;

        private float _startValue;
        private float _endValue;
        private float _interval;

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

        public void SetTargets (float start, float end, float interval)
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
                if (_interval == default)
                {
                    _callback?.Invoke (value);
                }
                else
                {
                    var progressPastLastInterval = 
                        Math.Abs (_startValue - _endValue) * _normalizedProgress >= _intervalProgress + _interval;
                    
                    if (progressPastLastInterval)
                    {
                        _intervalProgress += _interval;

                        _callback?.Invoke (value);
                    }
                }
            }
        }
    }
}