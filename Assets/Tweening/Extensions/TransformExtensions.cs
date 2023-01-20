using UnityEngine;

public static class TransformExtensions
{
    public static void SetPosition (this Transform transform, Vector3 position, Space space)
    {
        if (space.Equals (Space.Self))
            transform.localPosition = position;
        else
            transform.position = position;
    }

    public static void SetRotation (this Transform transform, Vector3 rotation, Space space)
    {
        if (space.Equals (Space.Self))
            transform.localRotation = Quaternion.Euler (rotation);
        else
            transform.rotation = Quaternion.Euler (rotation);
    }

    public static Vector3 GetPosition (this Transform transform, Space space)
    {
        return space.Equals (Space.World) ? transform.position : transform.localPosition;
    }

    public static Vector3 GetRotation (this Transform transform, Space space)
    {
        return space.Equals (Space.World) ? transform.rotation.eulerAngles : transform.localRotation.eulerAngles;
    }
}