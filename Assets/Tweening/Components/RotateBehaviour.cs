using UnityEngine;

namespace JK.Tweening
{
    public class RotateBehaviour : TransformTweenBehaviourBase
    {
        public override void Play ()
        {
            OriginalVector = TargetTransform.GetRotation (TweeningSpace);

            switch (base.TweenType)
            {
                case TweenType.FromTo:
                    ActiveTween = TargetTransform.RotateFromTo (StartVector, EndVector, Duration, TweeningSpace);
                    break;
                case TweenType.From:
                    ActiveTween = TargetTransform.RotateFrom (StartVector, Duration, TweeningSpace);
                    break;
                case TweenType.To:
                    ActiveTween = TargetTransform.RotateTo (EndVector, Duration, TweeningSpace);
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
                transform.SetRotation (StartVector, TweeningSpace);
            }

            base.Stop ();
        }
    }
}