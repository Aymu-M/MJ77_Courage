using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class movingPlatform : MonoBehaviour
{
    public float mvSpd = 4, curSpd;
    public Rigidbody thisRB;
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
    public float accel;
    IEnumerator fncMove()
    {
        yield return null;
        print("START MOVE");
        isMove = true;
        curSpd = Vector3.Distance(transform.position, waypoints[waypointsIndex].pos) / waypoints[waypointsIndex].travelTime;
        while (isMove)
        {
            fncChkwaypoints();
            fncGetMove();
            accel = accel > 1 ? 1 : accel + Time.fixedDeltaTime;
            // transform.position = Vector3.SmoothDamp(transform.position, waypoints[waypointsIndex].pos,
            //  ref velocity, waypoints[waypointsIndex].travelTime, mvSpd);
            yield return new WaitForFixedUpdate();
        }
        isMove = false;
        print("END MOVE");
    }
    public void fncChkwaypoints()
    {
        if ((transform.position - waypoints[waypointsIndex].pos).sqrMagnitude < .5)
        {
            waypointsIndex = (waypointsIndex + 1) >= waypoints.Count ? 0 : waypointsIndex + 1;
            accel = 0;
            curSpd = Vector3.Distance(transform.position, waypoints[waypointsIndex].pos) / waypoints[waypointsIndex].travelTime;
        }
    }
    public void fncGetMove()
    {
        // thisRB.MovePosition(thisRB.position + ((waypoints[waypointsIndex].pos - thisRB.position).normalized * (curSpd * (accel * accel))));
        thisRB.MovePosition(Vector3.SmoothDamp(thisRB.position, waypoints[waypointsIndex].pos,
             ref velocity, waypoints[waypointsIndex].travelTime, mvSpd));
        print("MOVING");
    }
}