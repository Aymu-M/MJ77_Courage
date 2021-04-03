using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
   public Transform camRig;
    public float rotSpd;
    // Start is called before the first frame update
    void Start()
    {
        fncGetTgtRot(transform.eulerAngles);
    }
    Vector3 camRot;
    public Quaternion targetRot, avatarRot;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // camRot = transform.eulerAngles + (new Vector3(Input.GetAxis("Mouse Y") * -rotSpd, Input.GetAxis("Mouse X") * rotSpd, 0));
            // targetRot = Quaternion.Euler(camRot);
            fncGetTgtRot(transform.eulerAngles + (new Vector3(Input.GetAxis("Mouse Y") * -rotSpd, Input.GetAxis("Mouse X") * rotSpd, 0)));
        }
    }
    void fncGetTgtRot(Vector3 getTgt)
    {
        camRot = getTgt;
        targetRot = Quaternion.Euler(camRot);
        avatarRot = Quaternion.Euler(new Vector3(0, getTgt.y, 0));
    }
    void FixedUpdate()
    {
        if (transform.rotation != targetRot)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 15);
    }
}
