using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cameraToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = (Camera)FindObjectOfType(typeof(Camera));
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraToLookAt.transform);
    }
}
