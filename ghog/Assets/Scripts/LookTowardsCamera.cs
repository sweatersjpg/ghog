using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsCamera : MonoBehaviour
{
    Camera mainCamera;
    
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
