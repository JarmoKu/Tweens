using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTester : MonoBehaviour
{
    [Min (0f)]
    [SerializeField] private float m_progress = default;
    [SerializeField] private Vector3 m_startRotation = default;
    [SerializeField] private Vector3 m_targetRotation = default;

    private void OnValidate ()
    {
        transform.eulerAngles = Vector3.LerpUnclamped (m_startRotation, m_targetRotation, m_progress);
    }
}