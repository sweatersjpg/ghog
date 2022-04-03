using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 target;

    public GameObject lookTargetObject;

    Vector3 currentLook;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;

        currentLook = lookTargetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookTarget = lookTargetObject.transform.position + new Vector3(0, 2, 0);

        currentLook += (lookTarget - currentLook) / 32;

        transform.LookAt(currentLook);

        transform.position += (target - transform.position) / 32;
    }

    public void MoveTo(Vector3 newPos)
    {
        target = newPos + new Vector3(0, 0, 0);
    }
}
