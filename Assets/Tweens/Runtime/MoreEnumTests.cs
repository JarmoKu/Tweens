using JK.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreEnumTests : MonoBehaviour
{
    [SerializeField] private LoopType m_loopTypeOne;
    [SerializeField] private LoopType m_loopTypeTwo;
    [SerializeField] private TweenType m_tweenTypeOne;
    [SerializeField] private TweenType m_tweenTypeTwo;

    private void OnEnable ()
    {
        //Debug.Log ($"1 {m_loopTypeOne.IsMatch (m_loopTypeTwo)}");
        //Debug.Log ($"2 {m_tweenTypeOne.IsMatch (m_tweenTypeTwo)}");
    }

    private void Update ()
    {
        for (int i = 0; i < 100; i++)
        {
            //m_loopTypeOne.IsMatch (m_loopTypeTwo);
            m_loopTypeOne.Matches (m_loopTypeTwo);
        }

        //var a = m_loopTypeOne.IsMatch (m_loopTypeTwo);
        //var b = m_tweenTypeOne.IsMatch (m_tweenTypeTwo);
    }
}