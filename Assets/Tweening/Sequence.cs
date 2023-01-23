using System.Collections.Generic;

namespace JK.Tweening
{
    public interface IProgressable
    {
        public bool IsPlaying { get; }
        public void Update (float deltaTime);
    }

    public class Sequence : IProgressable
    {
        private readonly List<SequenceTween> tweens = new ();

        public bool IsPlaying { get; private set; }

        private float _totalDuration;
        private float _totalProgress;

        public void Update (float deltaTime)
        {
            _totalProgress += deltaTime;

            foreach (var sequenceTween in tweens)
            {
                var pastStartingTime = sequenceTween.StartTime <= _totalProgress;
                var beforeEndTime = sequenceTween.EndTime > _totalProgress;
                var hasNotStarted = !sequenceTween.Tween.IsPlaying;

                if (pastStartingTime && beforeEndTime && hasNotStarted)
                {
                    sequenceTween.Tween.Play ();
                }
            }

            var completed = _totalDuration <= _totalProgress;

            if (completed)
            {
                IsPlaying = false;
                TweenManager.RemoveTween (this);
            }
        }

        public void Play ()
        {
            IsPlaying = true;
            TweenManager.AddTween (this);
        }

        public void Pause ()
        {
            foreach (var tween in tweens)
            {
                tween.Tween.Pause ();
            }

            IsPlaying = false;
        }

        public void Reset ()
        {
            foreach (var tween in tweens)
            {
                tween.Tween.Reset ();
            }
        }

        public void Restart ()
        {
            Reset ();
            Play ();
        }

        public void Append (TweenBase tween)
        {
            tweens.Add (new SequenceTween (tween, _totalDuration));
            _totalDuration += tween.Duration;
        }

        public void Join (TweenBase tween, float startTime)
        {
            var newTween = new SequenceTween (tween, startTime);
            tweens.Add (newTween);

            var timeToAdd = newTween.EndTime > _totalDuration ? newTween.EndTime - _totalDuration : 0f;
            _totalDuration += timeToAdd;
        }

        public void AddDelay (float duration)
        {
            var delay = new FloatTween (0f, 1f, duration);
            tweens.Add (new SequenceTween (delay, _totalDuration));
            _totalDuration += duration;
        }

        public void Clear ()
        {
            _totalProgress = 0f;
            _totalDuration = 0f;
            tweens.Clear ();
        }

        public class SequenceTween
        {
            public TweenBase Tween;
            public float StartTime;

            public float EndTime => StartTime + Tween.Duration;

            public SequenceTween (TweenBase tweenBase, float startTime)
            {
                Tween = tweenBase;
                StartTime = startTime;
            }
        }
    }
}