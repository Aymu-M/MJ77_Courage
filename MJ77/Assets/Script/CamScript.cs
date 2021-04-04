using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
   public Transform camRig, locPlyr;
    public float rotSpd = 17, pitchLimit=45;
    // Start is called before the first frame update
    void Start()
    {
        fncGetTgtRot(Vector3.zero);//transform.eulerAngles);
    }
    Vector3 camRot;
    public Quaternion targetRot;
    public Quaternion avatarRot(Quaternion startRot) => Quaternion.Euler(0, camRot.y, 0);
    // Quaternion.RotateTowards(startRot, Quaternion.Euler(0, targetRot.y, 0),5);
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            fncGetTgtRot((new Vector3((Input.GetAxis("Mouse Y") * -rotSpd), (Input.GetAxis("Mouse X") * rotSpd), 0)));//(new Vector3(transform.eulerAngles.x + (Input.GetAxis("Mouse Y") * -rotSpd), transform.eulerAngles.y + (Input.GetAxis("Mouse X") * rotSpd), 0)));
        }
            // camRot = transform.eulerAngles + (new Vector3(Input.GetAxis("Mouse Y") * -rotSpd, Input.GetAxis("Mouse X") * rotSpd, 0));
            // targetRot = Quaternion.Euler(camRot);
    }
    void fncGetTgtRot(Vector3 getTgt)
    {
        float lowerPitch = 360 - pitchLimit;
        camRot = getTgt+transform.eulerAngles;
        camRot.z = 0;
        //  camRot.x = (camRot.x > pitchLimit) ? pitchLimit : ((camRot.x < -pitchLimit) ? -pitchLimit : pitchLimit); 
        // camRot.x = Mathf.Clamp(camRot.x, -pitchLimit, pitchLimit);
        // camRot.x = (camRot.x <pitchLimit)&&!(camRot.x>lowerPitch) ? (camRot.x > pitchLimit ? pitchLimit : camRot.x) : (camRot.x <0 ? (360 - pitchLimit) : camRot.x);
        camRot.x = (camRot.x > 180) ? (camRot.x < lowerPitch ? lowerPitch : camRot.x) : (camRot.x > pitchLimit ? pitchLimit : camRot.x);
        float qLim = pitchLimit / 360;
        targetRot = Quaternion.Euler(camRot);
        // targetRot.x = 
        // avatarRot = Quaternion.Euler(new Vector3(0, getTgt.y, 0));
    }

    Vector3 chaseVel;
    void FixedUpdate()
    {
        if(camRig.position!=locPlyr.position)
            transform.position = locPlyr.position;//Vector3.SmoothDamp(transform.position, locPlyr.position, ref chaseVel, .5f, 20, Time.fixedDeltaTime);
        if (camRig.rotation != targetRot)
            camRig.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 1);
    }
    
}
