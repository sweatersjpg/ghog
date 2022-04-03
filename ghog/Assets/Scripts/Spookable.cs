using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spookable : MonoBehaviour
{

    SpriteRenderer sr;
    public Color[] colorToggle;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered!");
        isActive = !isActive;

        if (isActive) sr.color = colorToggle[1];
        else sr.color = colorToggle[0];
    }
}
