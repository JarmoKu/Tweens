using UnityEngine;

namespace JK.Tweening
{
    public class TweenUtility
    {
        public static float Interpolate (float progress, float duration, LoopType loopType, EaseType easeType)
        {
            //System.Enum.IsDefined (loopType.GetType (), loopType);
            var normalizedProgress = 0f;
            switch (loopType)
            {
                case LoopType.None:
                    normalizedProgress = Mathf.Lerp (0f, 1f, progress / duration);
                    break;
                case LoopType.Repeat:
                    normalizedProgress = (progress / duration) % 1f;
                    break;
                case LoopType.PingPong:
                    normalizedProgress = Mathf.PingPong (progress * 2f / duration, 1f);
                    break;
                case LoopType.Additive:
                    normalizedProgress = (progress / duration) % 1f;
                    break;
                default:
                    Debug.Log ($"Incorrect LoopType {loopType}");
                    break;
            }

            return Ease.GetEased (normalizedProgress, easeType);
        }

        public static Vector3 QuadraticPosition (float progress, Vector3 start, Vector3 peak, Vector3 end)
        {
            return Mathf.Pow (1 - progress, 2) * start + 2 * progress * (1 - progress) * peak + Mathf.Pow (progress, 2) * end;
        }

        public static Vector3 InterpolateVector3 (Vector3 start, Vector3 end, float duration, float normalizedProgress, float totalProgress, LoopType loopType)
        {
            return loopType.Matches (LoopType.Additive) ? Vector3.LerpUnclamped (start, end, totalProgress / duration) : Vector3.Lerp (start, end, normalizedProgress);
        }
    }
}