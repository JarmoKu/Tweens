using UnityEngine;

namespace JK.Tweening
{
    public class PunchBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private PunchType m_punchType;

        public override void Play ()
        {
            RefreshStart ();

            switch (m_punchType)
            {
                case PunchType.Position:
                    ActiveTween = TargetTransform.PunchPosition (EndVector, Duration, TweeningSpace);
                    break;
                case PunchType.Rotation:
                    ActiveTween = TargetTransform.PunchRotation (EndVector, Duration, TweeningSpace);
                    break;
                case PunchType.Scale:
                    ActiveTween = TargetTransform.PunchScale (EndVector, Duration);
                    break;
                default:
                    Debug.Log ($"Incorrect PunchType {m_punchType}");
                    break;
            }

            base.Play ();
        }

        public override void Stop ()
        {
            switch (m_punchType)
            {
                case PunchType.Position:
                    transform.SetPosition (StartVector, TweeningSpace);
                    break;
                case PunchType.Rotation:
                    transform.SetRotation (StartVector, TweeningSpace);
                    break;
                case PunchType.Scale:
                    transform.localScale = StartVector;
                    break;
                default:
                    Debug.Log ($"Incorrect PunchType {m_punchType}");
                    break;
            }

            base.Stop ();
        }

        public void RefreshStart ()
        {
            switch (m_punchType)
            {
                case PunchType.Position:
                    StartVector = TargetTransform.GetPosition (TweeningSpace);
                    break;
                case PunchType.Rotation:
                    StartVector = TargetTransform.GetRotation (TweeningSpace);
                    break;
                case PunchType.Scale:
                    StartVector = TargetTransform.localScale;
                    break;
                default:
                    Debug.Log ($"Incorrect PunchType {m_punchType}");
                    break;
            }
        }
    }
}