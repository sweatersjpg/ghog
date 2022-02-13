using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera cameraToLookAt;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {

        cameraToLookAt = (Camera)FindObjectOfType(typeof(Camera));

        if (sprites.Length > 0)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(cameraToLookAt.transform);
        transform.rotation = cameraToLookAt.transform.rotation;
    }
}
