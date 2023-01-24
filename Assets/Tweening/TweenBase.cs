using UnityEngine;

namespace JK.Tweening
{
	public abstract class TweenBase : IProgressable
	{
        public delegate void TweenEventHandler ();

        public event TweenEventHandler OnUpdated;
        public event TweenEventHandler OnCompleted;

        public bool IsCompleted { get; private set; }
		public bool IsPlaying { get; private set; }

		protected float _loopDelay;
		protected float _loopDelayLeft;
		protected float _loopsDone;

		protected float _totalProgress;
		protected float _normalizedProgress;
		protected float _duration;

		protected int _currentLoop;
		protected int _loopCount;
		protected LoopType _loopType = LoopType.None;
		protected EaseType _easeType = EaseType.Linear;

        public float Duration => _duration;
        public abstract void Update (float deltaTime);
        public abstract void Reset ();

        public void Play ()
		{
			IsPlaying = true;
			TweenManager.AddTween (this);
		}

        public void Pause ()
        {
            IsPlaying = false;
        }

        public void Restart ()
		{
			BaseReset ();
			Play ();
		}

        public void SetEase (EaseType easeType)
        {
            _easeType = easeType;
        }

        public virtual void SetLoops (int count, LoopType type = LoopType.None)
		{
			_loopCount = count;
			_loopType = type;
		}

		public void SetLoopDelay (float delay)
		{
			_loopDelay = delay;
			_loopDelayLeft = 0f;
		}

		protected bool TryProgress (float deltaTime, out bool completedLoop)
		{
            completedLoop = _totalProgress > _duration * (_loopsDone + 1);

			if (_loopDelayLeft > 0f)
			{
				_loopDelayLeft -= deltaTime;
				return false;
			}

			Progress (deltaTime);

            if (completedLoop) 
                OnLoopCompleted ();

            return true;
		}

        protected void BaseReset ()
        {
            _totalProgress = 0f;
            _currentLoop = 0;
            _loopsDone = 0;
            _loopDelayLeft = 0f;
            _normalizedProgress = 0f;

            IsPlaying = false;
            IsCompleted = false;
        }

		private void Progress (float scaledDeltatime)
		{
            _totalProgress += scaledDeltatime;
            _normalizedProgress = TweenUtility.Interpolate (_totalProgress, _duration, _loopType, _easeType);

            OnUpdated?.Invoke ();
        }

		private void OnLoopCompleted ()
		{
            if (_loopCount != -1)
                _totalProgress = Mathf.Min (_totalProgress, _duration * _loopCount);

            _normalizedProgress = _loopType.Matches (LoopType.PingPong) ? 0f : 1f;

            _loopsDone++;
            _loopDelayLeft = _loopDelay;

            var allLoopsCompleted = _loopsDone >= _loopCount && _loopCount != -1;
            if (allLoopsCompleted)
                OnAllLoopsCompleted ();
        }

        private void OnAllLoopsCompleted ()
        {
            IsPlaying = false;
            IsCompleted = true;
            TweenManager.RemoveTween (this);

            OnCompleted?.Invoke ();
        }
	}
}