using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public Rigidbody thisRB;
    public float CurMove, MvSpd, SprintSpd, RotSpd, Accelerate;
    public float JmpPwr, JmpCntr, JmpTime;

    public bool isMove, isJump, isClimb;

    Vector3 getInputMove { get { return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); } }
    Vector3 inputMove, getRot;
    Quaternion targetRot;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputMove = getInputMove;
        isMove = inputMove.sqrMagnitude >= .1f;
        isJump = Input.GetAxis("Jump") >= .1f;
        CurMove = Input.GetKey(KeyCode.LeftShift) ? SprintSpd : MvSpd;
        // if (isMove)
        // {
        //     numAccel = CurMove != (Input.GetKey(KeyCode.LeftShift) ? SprintSpd : MvSpd) ? 0 : numAccel + Time.deltaTime;
        // }
        // else
        // { numAccel -= Time.deltaTime; }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            getRot.x = thisRB.rotation.x + (Input.GetAxis("Mouse Y") * RotSpd);
            getRot.y = thisRB.rotation.y + (Input.GetAxis("Mouse X") * RotSpd);
            targetRot = Quaternion.Euler(getRot);
            rotTime = 0;
        }
    }
    float rotTime;
    void FixedUpdate()
    {
        if (isMove)
            thisRB.velocity = (inputMove * CurMove);// * Time.fixedDeltaTime;
        if (isJump && JmpCntr < JmpTime)
        {
            float t = JmpCntr / JmpTime, getJump = Mathf.Lerp(JmpPwr, 0, t);
            thisRB.AddForce(new Vector3(0, getJump, 0),ForceMode.VelocityChange);
            JmpCntr += Time.deltaTime;
        }
        else
        {
            if (JmpCntr > 0)
                JmpCntr -= Time.deltaTime * 2;
        }
        if (thisRB.rotation != targetRot)
        {
            thisRB.MoveRotation(targetRot);
        }
    }
}