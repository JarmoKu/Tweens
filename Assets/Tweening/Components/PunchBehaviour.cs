using UnityEngine;

namespace JK.Tweening
{
    public class PunchBehaviour : TransformTweenBehaviourBase
    {
        [Space]
        [SerializeField] private PunchType m_punchType;
        [SerializeField] private Vector3 m_start;
        [SerializeField] private Vector3 m_target;

        public override void Play ()
        {
            RefreshStart ();

            switch (m_punchType)
            {
                case PunchType.Position:
                    ActiveTween = TargetTransform.PunchPosition (m_target, Duration, TweeningSpace);
                    break;
                case PunchType.Rotation:
                    ActiveTween = TargetTransform.PunchRotation (m_target, Duration, TweeningSpace);
                    break;
                case PunchType.Scale:
                    ActiveTween = TargetTransform.PunchScale (m_target, Duration);
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
                    transform.SetPosition (m_start, TweeningSpace);
                    break;
                case PunchType.Rotation:
                    transform.SetRotation (m_start, TweeningSpace);
                    break;
                case PunchType.Scale:
                    transform.localScale = m_start;
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
                    m_start = TargetTransform.GetPosition (TweeningSpace);
                    break;
                case PunchType.Rotation:
                    m_start = TargetTransform.GetRotation (TweeningSpace);
                    break;
                case PunchType.Scale:
                    m_start = TargetTransform.localScale;
                    break;
                default:
                    Debug.Log ($"Incorrect PunchType {m_punchType}");
                    break;
            }
        }
    }
}