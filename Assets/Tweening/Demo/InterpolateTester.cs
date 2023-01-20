using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterpolateTester : MonoBehaviour
{
    [Range (0f, 1f)]
    [SerializeField] private float m_progress;
    [SerializeField] private float m_duration;
    [SerializeField] private LoopType m_loopType;
    [SerializeField] private EaseType m_easeType;
    [Space]
    [SerializeField] private float m_currentValue;

    private void OnValidate ()
    {
        m_currentValue = TweenUtility.Interpolate (m_progress * m_duration, m_duration, m_loopType, m_easeType);
    }
}