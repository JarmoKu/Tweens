using UnityEngine;

namespace JK.Tweening
{
    public class MoveBehaviour : TransformTweenBehaviourBase
    {
        public override void Play ()
        {
            switch (base.TweenType)
            {
                case TweenType.FromTo:
                    ActiveTween = TargetTransform.MoveFromTo (StartVector, EndVector, Duration, TweeningSpace);
                    break;
                case TweenType.From:
                    ActiveTween = TargetTransform.MoveFrom (StartVector, Duration, TweeningSpace);
                    break;
                case TweenType.To:
                    ActiveTween = TargetTransform.MoveTo (EndVector, Duration, TweeningSpace);
                    break;
                default:
                    Debug.LogError ("No tween type found");
                    break;
            }

            base.Play ();
        }

        public override void Stop ()
        {
            if (ActiveTween == null)
            {
                transform.position = StartVector;
            }

            base.Stop ();
        }
    }
}