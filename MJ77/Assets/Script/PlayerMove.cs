using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public Transform locGround;
    public Rigidbody thisRB, platformRB;
    public CamScript CamSys;
    // public float Breath;
    public float CurMove, MvSpd, SprintSpd, RotSpd, Accelerate;
    public float JmpPwr, JmpCntr, JmpTime;

    public bool isMove, isJump, isClimb, isGround, inPlatform;

    Vector3 getInputMove { get { return new Vector3(Input.GetAxis("Horizontal"), 0, (Input.GetAxis("Vertical"))); } }
    Vector3 inputMove, getRot, getMove;
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
        isJump = Input.GetKey(KeyCode.Space);//Input.GetAxis("Jump") >= .1f;
        CurMove = Input.GetKey(KeyCode.LeftShift) ? MvSpd : MvSpd;
        // if (isMove)
        // {
        //     numAccel = CurMove != (Input.GetKey(KeyCode.LeftShift) ? SprintSpd : MvSpd) ? 0 : numAccel + Time.deltaTime;
        // }
        // else
        // { numAccel -= Time.deltaTime; }

        if (Input.GetAxis("Mouse X") != 0)
        {
            // getRot.x = thisRB.rotation.x + (Input.GetAxis("Mouse Y") * -RotSpd);
            getRot = Vector3.zero;
            getRot.y = CamSys.avatarRot(transform.rotation).eulerAngles.y;//CamSys.camRig.eulerAngles.y;//thisRB.rotation.y + (Input.GetAxis("Mouse X") * CamSys.rotSpd);
            targetRot = CamSys.avatarRot(transform.rotation);//Quaternion.Euler();//thisRB.rotation.eulerAngles + getRot);
        }


    }
    void FixedUpdate()
    {
        if (isMove)
        {
            getMove = CamSys.camRig.TransformDirection(inputMove * CurMove);// Mathf.Lerp(0, CurMove, Accelerate * Accelerate));
            thisRB.MovePosition(transform.localPosition + (getMove * CurMove*Time.fixedDeltaTime));
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
            // print($"Movement : {getMove} by {inputMove}");
            // if (thisRB.velocity.magnitude > MvSpd)
            // {
            //     getMove.x = Mathf.Abs(thisRB.velocity.x) > MvSpd ? (thisRB.velocity.x - getMove.x) : getMove.x;
            //     getMove.y = Mathf.Abs(thisRB.velocity.y) > MvSpd ? (thisRB.velocity.y - getMove.y) : getMove.y;
            // }
            // thisRB.AddForce(getMove, ForceMode.Impulse);
            // if (thisRB.velocity.magnitude > MvSpd)
            //     thisRB.velocity = new Vector3(thisRB.velocity.normalized.x * CurMove,
            //     thisRB.velocity.y, thisRB.velocity.normalized.z * CurMove);
            // if (Accelerate <= 1)
            //     Accelerate += Time.deltaTime;
            // else Accelerate = 1;
            Accelerate = Accelerate > 1 ? 1 : Accelerate + (Time.fixedDeltaTime*2);
        }
        else
        {
            if (Accelerate > 0) Accelerate -= Time.deltaTime;
            else if (Accelerate < 0)
                Accelerate = 0;
            if (thisRB.velocity.sqrMagnitude > 0)
                thisRB.velocity = new Vector3(thisRB.velocity.x * .5f, thisRB.velocity.y, thisRB.velocity.z * .5f);
        }
        // if(inPlatform&&isGround){
        //     thisRB.velocity = thisRB.velocity+ platformRB.velocity;
        // }
        if (isJump && (JmpCntr < JmpTime))
        {
            print($"jump: {isJump} at {Time.time.ToString("##.#")}");
            isGround = false;
            float t = JmpCntr / JmpTime;//, getJump = Mathf.Lerp(JmpPwr, 0, t);
            // getMove.y = Mathf.Lerp(JmpPwr, 0, t);// thisRB.AddForce(new Vector3(0, getJump, 0),ForceMode.VelocityChange);
            JmpCntr += Time.deltaTime;
            thisRB.AddForce(new Vector3(0, Mathf.Lerp(JmpPwr, 0, t), 0), ForceMode.Impulse);
        }
        else
        {
            if (JmpCntr > 0&&isGround)
                JmpCntr -=  Time.deltaTime * 2;
        }


        // thisRB.velocity = getMove;
        // if(isMove&& (isJump&&JmpCntr<JmpTime))
        //     thisRB.AddForce(getMove, ForceMode.VelocityChange);

        // if (thisRB.rotation != targetRot)
        // {
        //     thisRB.MoveRotation(targetRot);
        // }
        // if(transform.eulerAngles.y!=CamSys.targetRot.y)
        //     transform.Rotate(CamSys.avatarRot(transform.rotation).eulerAngles);
        // thisRB.MoveRotation(CamSys.avatarRot(transform.rotation));//Quaternion.Slerp(thisRB.rotation, CamSys.avatarRot, CamSys.rotSpd);
        // if (transform.eulerAngles.y != getRot.y)
        //     transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, getRot, ref deltaRot, .3f, CamSys.rotSpd);
        // targetRot;//Quaternion.Lerp(thisRB.rotation, targetRot, Time.fixedDeltaTime);
        if(transform.rotation!=targetRot)
            transform.rotation = targetRot;
    }
    Vector3 deltaRot;

    void OnCollisionStay(Collision col) {
        // if((transform.position-col.collider.ClosestPoint(transform.position)).sqrMagnitude<GetComponent<Collider>().bounds.extents.sqrMagnitude)
        //     thisRB.velocity = Vector3.down;
        isGround = (col.collider.ClosestPoint(transform.position).y<=transform.position.y);
    }

    public void fncGetPlatform(Rigidbody thisPlatform){
        // transform.SetParent(thisPlatform.transform);
        platformRB = thisPlatform;
        inPlatform = true;
        print($"In platform, {thisPlatform.name} at {Time.time.ToString("##.#")}");
    }
    public void fncRemPlatform(Rigidbody thisPlatform){
        // transform.parent = null;
        platformRB = null;
        inPlatform = false;
        print($"Out of platform, {thisPlatform.name} at {Time.time.ToString("##.#")}");
    }
}