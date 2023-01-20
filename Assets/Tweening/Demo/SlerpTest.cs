using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpTest : MonoBehaviour
{
    public Transform sunrise;
    public Transform sunset;
    public Vector3 upDirection;
    public float height;
    public Vector3 Offset;

    [Range (0f, 1f)]
    public float Progress;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    void Start ()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;
    }

    void Update ()
    {
        // The center of the arc
        Vector3 center = (sunrise.position + sunset.position) * 0.5F;

        // move the center a bit downwards to make the arc vertical
        center -= Offset;

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;

        transform.position = Vector3.Slerp (riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }

    Vector3 p0; // start
    Vector3 c0; // middle
    Vector3 p1; // end

    private Vector3 GetQuadraticCoordinates (float t)
    {
        return Mathf.Pow (1 - t, 2) * sunrise.position + 2 * t * (1 - t) * Offset + Mathf.Pow (t, 2) * sunset.position;
    }

    private void OnValidate ()
    {
        transform.position = GetQuadraticCoordinates (Progress);
    }
}