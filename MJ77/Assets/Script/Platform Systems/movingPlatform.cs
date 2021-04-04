using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movingPlatform : MonoBehaviour
{
    public List<Transform> waypoint;
    public int waypointIndex;
    IEnumerator moveOperation;
    bool isMove;
    // Start is called before the first frame update
    void Start()
    {

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
        isMove=true;
        while (isMove)
        {
            fncChkWaypoint();
            transform.position = Vector3.SmoothDamp(transform.position, waypoint[waypointIndex].position, ref velocity, 5, 1);
            yield return new WaitForFixedUpdate();
        }
        isMove=false;
    }
    public void fncChkWaypoint(){
        if((transform.position-waypoint[waypointIndex].position).sqrMagnitude<.5)
            waypointIndex = (waypointIndex + 1) >= waypoint.Count ? 0 : waypointIndex+1;
    }
}
