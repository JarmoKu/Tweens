using System;
using UnityEngine;

public static class Ease
{
    public static float GetEased (float value, EaseType easeType)
    {
        switch (easeType)
        {
            case EaseType.Linear: return Linear (value);
            case EaseType.EaseInSine: return EaseInSine (value);
            case EaseType.EaseOutSine: return EaseOutSine (value);
            case EaseType.EaseInOutSine: return EaseInOutSine (value);
            case EaseType.EaseInQuad: return EaseInQuad (value);
            case EaseType.EaseOutQuad: return EaseOutQuad (value);
            case EaseType.EaseInOutQuad: return EaseInOutQuad (value);
            case EaseType.EaseInCubic: return EaseInCubic (value);
            case EaseType.EaseOutCubic: return EaseOutCubic (value);
            case EaseType.EaseInOutCubic: return EaseInOutCubic (value);
            case EaseType.EaseInQuart: return EaseInQuart (value);
            case EaseType.EaseOutQuart: return EaseOutQuart (value);
            case EaseType.EaseInOutQuart: return EaseInOutQuart (value);
            case EaseType.EaseInQuint: return EaseInQuint (value);
            case EaseType.EaseOutQuint: return EaseOutQuint (value);
            case EaseType.EaseInOutQuint: return EaseInOutQuint (value);
            case EaseType.EaseInExpo: return EaseInExpo (value);
            case EaseType.EaseOutExpo: return EaseOutExpo (value);
            case EaseType.EaseInOutExpo: return EaseInOutExpo (value);
            case EaseType.EaseInCirc: return EaseInCirc (value);
            case EaseType.EaseOutCirc: return EaseOutCirc (value);
            case EaseType.EaseInOutCirc: return EaseInOutCirc (value);
            case EaseType.EaseInBack: return EaseInBack (value);
            case EaseType.EaseOutBack: return EaseOutBack (value);
            case EaseType.EaseInOutBack: return EaseInOutBack (value);
            case EaseType.EaseInElastic: return EaseInElastic (value);
            case EaseType.EaseOutElastic: return EaseOutElastic (value);
            case EaseType.EaseInOutElastic: return EaseInOutElastic (value);
            case EaseType.EaseInBounce: return EaseInBounce (value);
            case EaseType.EaseOutBounce: return EaseOutBounce (value);
            case EaseType.EaseInOutBounce: return EaseInOutBounce (value);
            case EaseType.Spike: return Spike (value);
            case EaseType.InPunch: return InPunch (value);
            case EaseType.OutPunch: return OutPunch (value);
            case EaseType.Shake: return Shake (value);
            default: Debug.LogError ($"No Ease Type {easeType} Found"); 
                return -1f;
        }
    }

    public static float Linear (float t) => t;

    public static float EaseInSine (float value) => 1f - Mathf.Cos (value * Mathf.PI / 2f);

    public static float EaseOutSine (float value) => Mathf.Sin (value * Mathf.PI / 2f);

    public static float EaseInOutSine (float value) => -(Mathf.Cos (Mathf.PI * value) - 1f) / 2f;

    public static float EaseInQuad (float value) => value * value;

    public static float EaseOutQuad (float value) => 1f - (1f - value) * (1f - value);

    public static float EaseInOutQuad (float value) => 
        value < 0.5 ? 2 * value * value : 1 - Mathf.Pow (-2f * value + 2f, 2f) / 2f;

    public static float EaseInCubic (float value) => value * value * value;

    public static float EaseOutCubic (float value) => 1f - Mathf.Pow (1f - value, 3f);

    public static float EaseInOutCubic (float value) => 
        value < 0.5 ? 4 * value * value * value : 1f - Mathf.Pow (-2f * value + 2f, 3f) / 2f;

    public static float EaseInQuart (float value) => value * value * value * value;

    public static float EaseOutQuart (float value) => 1f - Mathf.Pow (1f - value, 4f);

    public static float EaseInOutQuart (float value) => 
        value < 0.5 ? 8 * value * value * value * value : 1f - Mathf.Pow (-2f * value + 2f, 4f) / 2f;

    public static float EaseInQuint (float value) => value * value * value * value * value;

    public static float EaseOutQuint (float value) => 1f - Mathf.Pow (1f - value, 5f);

    public static float EaseInOutQuint (float value) => 
        value < 0.5f ? 16f * value * value * value * value * value : 1f - Mathf.Pow (-2f * value + 2f, 5f) / 2f;

    public static float EaseInExpo (float value) => value == 0f ? 0f : Mathf.Pow (2f, 10f * value - 10f);

    public static float EaseOutExpo (float value) => value == 1f ? 1f : 1f - Mathf.Pow (2f, -10f * value);

    public static float EaseInOutExpo (float value) => 
        value == 0f ? 0f : value == 1f ? 1f : value < 0.5f ? 
        Mathf.Pow (2f, 20f * value - 10f) / 2f : (2f - Mathf.Pow (2f, -20f * value + 10f)) / 2f;

    public static float EaseInCirc (float value) => 1f - Mathf.Sqrt (1f - Mathf.Pow (value, 2f));

    public static float EaseOutCirc (float value) => Mathf.Sqrt (1f - Mathf.Pow (value - 1f, 2f));

    public static float EaseInOutCirc (float value) => 
        value < 0.5f ? (1f - Mathf.Sqrt (1f - Mathf.Pow (2f * value, 2f))) / 2f : 
        (Mathf.Sqrt (1f - Mathf.Pow (-2f * value + 2f, 2f)) + 1f) / 2f;

    public static float EaseInBack (float value) => 2.70158f * value * value * value - 1.70158f * value * value;

    public static float EaseOutBack (float value) => 
        1f + 2.70158f * Mathf.Pow (value - 1f, 3f) + 1.70158f * Mathf.Pow (value - 1f, 2f);

    public static float EaseInOutBack (float value)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        return value < 0.5f ? (Mathf.Pow (2f * value, 2f) * ((c2 + 1f) * 2f * value - c2)) / 2f : 
            (Mathf.Pow (2f * value - 2f, 2f) * ((c2 + 1f) * (value * 2f - 2f) + c2) + 2f) / 2f;
    }

    public static float EaseInElastic (float value) => 1f - EaseOutElastic (1f - value);

    public static float EaseOutElastic (float value) => 
        (float) (Mathf.Pow (2f, -10f * value) * Mathf.Sin ((value - 0.3f / 4f) * (2f * Mathf.PI) / 0.3f) + 1f);

    public static float EaseInOutElastic (float value)
    {
        if (value < 0.5f) return EaseInElastic (value * 2f) / 2f;
        return 1f - EaseInElastic ((1f - value) * 2f) / 2f;
    }

    public static float EaseInBounce (float value) => 1f - EaseOutBounce (1f - value);

    public static float EaseOutBounce (float value)
    {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (value < 1 / d1)
            return n1 * value * value;
        else if (value < 2 / d1)
            return n1 * (value -= 1.5f / d1) * value + 0.75f;
        else if (value < 2.5 / d1)
            return n1 * (value -= 2.25f / d1) * value + 0.9375f;
        else
            return n1 * (value -= 2.625f / d1) * value + 0.984375f;
    }

    public static float EaseInOutBounce (float value) => 
        value < 0.5f ? (1f - EaseOutBounce (1f - 2f * value)) / 2f : (1f + EaseOutBounce (2f * value - 1f)) / 2f;

    public static float Spike (float t) => t < .5f ? EaseInQuad (t * .5f) * 2f : EaseInQuad ((1f - t) * .5f) * 2f;

    public static float InPunch (float t) => t < .1f ? EaseInQuad (t * .1f) * 2f : EaseInQuad ((1f - t) * .9f) * 2f;

    public static float OutPunch (float t) => t < .9f ? EaseInQuad (t * .9f) * 2f : EaseInQuad ((1f - t) * .1f) * 2f;

    public static float Shake (float t) => (t + (Mathf.Cos ((t - t) * Mathf.PI) * t) / 2f);
}