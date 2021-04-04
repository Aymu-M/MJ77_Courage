using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSys : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<PlayerMove>(out PlayerMove thisPlyr))
            thisPlyr.fncGetPlatform(transform);
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<PlayerMove>(out PlayerMove thisPlyr))
            thisPlyr.fncRemPlatform(transform);
    }
}
