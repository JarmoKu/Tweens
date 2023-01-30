using UnityEngine;

/*namespace JK.Tweening
{
    public class ScaleBehaviour : TransformTweenBehaviourBase
    {
        public override void Play ()
        {
            OriginalVector = TargetTransform.localScale;

            switch (base.TweenType)
            {
                case TweenType.FromTo:
                    ActiveTween = TargetTransform.ScaleFromTo (StartVector, EndVector, Duration);
                    break;
                case TweenType.From:
                    ActiveTween = TargetTransform.ScaleFrom (StartVector, Duration);
                    break;
                case TweenType.To:
                    ActiveTween = TargetTransform.ScaleTo (EndVector, Duration);
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
                TargetTransform.localScale = OriginalVector;
            }

            base.Stop ();
        }
    }
}*/