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

    Vector3 getInputMove { get { return new Vector3(Input.GetAxis("Horizontal"),0, (Input.GetAxis("Vertical"))); } }
    [SerializeField] Vector3 inputMove, getRot, getMove;
    Quaternion targetRot;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        targetRot = thisRB.rotation;
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
            // getRot.x = thisRB.rotation.x + (Input.GetAxis("Mouse Y") * -RotSpd);
            getRot.y = thisRB.rotation.y + (Input.GetAxis("Mouse X") * RotSpd);
            targetRot = Quaternion.Euler(thisRB.rotation.eulerAngles + getRot);
            rotTime = 0;
        }
    }
    float rotTime;
    void FixedUpdate()
    {
        if (isMove)
        {
            getMove = transform.TransformDirection(inputMove * Mathf.Lerp(0, CurMove, Accelerate * Accelerate));
            // thisRB.velocity = (inputMove * CurMove);// * Time.fixedDeltaTime;
            // if (thisRB.velocity.sqrMagnitude > getMove.sqrMagnitude)
            // {
            //     // getMove.x = Mathf.Clamp(getMove.x, -CurMove, CurMove);
            //     // getMove.z = Mathf.Clamp(getMove.z, -CurMove, CurMove);
            //     //getMove.y = 0;
            //     getMove.x = (thisRB.velocity.x - getMove.x); //(getMove.x>thisRB.velocity.x) ? 0 : getMove.x - thisRB.velocity.x;
            //     getMove.z = (thisRB.velocity.z - getMove.z);//(getMove.z > thisRB.velocity.z)?0 : getMove.z;
            // }
            // getMove.y = thisRB.velocity.y;
            print($"Movement : {getMove} by {inputMove}");
            thisRB.AddForce(getMove, ForceMode.Impulse);
            if (thisRB.velocity.magnitude > MvSpd)
                thisRB.velocity = new Vector3(thisRB.velocity.normalized.x * CurMove,
                thisRB.velocity.y, thisRB.velocity.normalized.z * CurMove);
            if (Accelerate <= 1)
                Accelerate += Time.deltaTime;
            else Accelerate = 1;
        }
        else
        {
            if (Accelerate > 0) Accelerate -= Time.deltaTime;
            else if (Accelerate < 0)
                Accelerate = 0;
            if (thisRB.velocity.sqrMagnitude > 0)
                thisRB.velocity = new Vector3(thisRB.velocity.x * .5f, thisRB.velocity.y, thisRB.velocity.z * .5f);
        }
        if (isJump && (JmpCntr < JmpTime))
        {
            print($"jump: {isJump}");
            float t = JmpCntr / JmpTime;//, getJump = Mathf.Lerp(JmpPwr, 0, t);
            // getMove.y = Mathf.Lerp(JmpPwr, 0, t);// thisRB.AddForce(new Vector3(0, getJump, 0),ForceMode.VelocityChange);
            JmpCntr += Time.deltaTime;
            thisRB.AddForce(new Vector3(0, Mathf.Lerp(JmpPwr, 0, t), 0), ForceMode.Impulse);
        }
        else
        {
            if (JmpCntr > 0)
                JmpCntr -= Time.deltaTime * 2;
        }


        // thisRB.velocity = getMove;
        // if(isMove&& (isJump&&JmpCntr<JmpTime))
        //     thisRB.AddForce(getMove, ForceMode.VelocityChange);

        if (thisRB.rotation != targetRot)
        {
            thisRB.MoveRotation(targetRot);
        }
    }
}