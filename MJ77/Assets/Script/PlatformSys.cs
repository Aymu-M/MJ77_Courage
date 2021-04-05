using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class PlatformSys : MonoBehaviour
{
    [Header("USE THE RIGIDBODY OF ACTUAL PLATFORM!!!")] public Rigidbody platformRB;
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<PlayerMove>(out PlayerMove thisPlyr))
        { thisPlyr.fncGetPlatform(platformRB); 
        // platformRB.collisionDetectionMode = CollisionDetectionMode
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<PlayerMove>(out PlayerMove thisPlyr))
            thisPlyr.fncRemPlatform(platformRB);
    }
}