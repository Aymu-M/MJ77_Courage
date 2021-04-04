using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movingPlatform : MonoBehaviour
{
    public float mvSpd=4;
    public List<waypointData> waypoints;
    public int waypointsIndex;
    IEnumerator moveOperation;
    bool isMove;
    [System.Serializable]
    public struct waypointData
    {
        public Transform locWaypoint;
        public Vector3 pos { get { return locWaypoint.position; } }
        public float travelTime;
    }
    // Start is called before the first frame update
    void Start()
    {
fncStartMove();
    }
    public void fncStartMove()
    {
        moveOperation = fncMove();
        StartCoroutine(moveOperation);
    }
    public void fncStopMove()
    {
        StopCoroutine(moveOperation);
    }
    Vector3 velocity;
    IEnumerator fncMove()
    {
        isMove = true;
        while (isMove)
        {
            fncChkwaypoints();
            transform.position = Vector3.SmoothDamp(transform.position, waypoints[waypointsIndex].pos,
             ref velocity, waypoints[waypointsIndex].travelTime, mvSpd);
            yield return new WaitForFixedUpdate();
        }
        isMove = false;
    }
    public void fncChkwaypoints()
    {
        if ((transform.position - waypoints[waypointsIndex].pos).sqrMagnitude < .5)
            waypointsIndex = (waypointsIndex + 1) >= waypoints.Count ? 0 : waypointsIndex + 1;
    }
}
