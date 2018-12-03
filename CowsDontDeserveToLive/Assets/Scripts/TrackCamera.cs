using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour 
{
    public Transform Camera;
    public Vector2 Offset;
    public float FollowUnits;

    // Update is called once per frame
    void Update () 
    {
        transform.position = new Vector3(Mathf.Round(Camera.position.x / FollowUnits) * FollowUnits, Mathf.Round(Camera.position.y / FollowUnits) * FollowUnits, 0) + (Vector3)Offset;
    }
}