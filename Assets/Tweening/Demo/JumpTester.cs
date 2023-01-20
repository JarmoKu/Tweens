using UnityEngine;

namespace JK.Tweening
{
    public class JumpTester : MonoBehaviour
    {
        [SerializeField] private Transform m_jumpStart;
        [SerializeField] private Transform m_jumpPeak;
        [SerializeField] private Transform m_jumpEnd;
        [SerializeField] private float m_jumpHeight;
        [SerializeField] private bool m_usePeak;
        [SerializeField] private LoopType m_loopType;
        [SerializeField] private int m_loopCount;

        private void OnEnable ()
        {
            if (m_usePeak)
            {
                var tween = transform.JumpFromTo (
                    m_jumpStart.position, m_jumpPeak.position, m_jumpEnd.position, 2f, Space.World);

                tween.SetLoops (m_loopCount, m_loopType);
                tween.Play ();
            }
            else
            {
                var tween = transform.JumpFromTo (
                    m_jumpStart.position, m_jumpEnd.position, m_jumpHeight, 2f, Vector3.up, Space.World);

                tween.SetLoops (m_loopCount, m_loopType);
                tween.Play ();
            }
        }
    }
}