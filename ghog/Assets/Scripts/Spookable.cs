using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spookable : MonoBehaviour
{

    Animator flame;
    public Color[] colorToggle;

    public Animation[] animationsToPlay;
    public GameObject[] objectsToActivate;
    public bool startObjectsAsDeactivated = false;
    public GameObject[] objectsToDeactivate;

    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        flame = GetComponentInChildren<Animator>();

        for (int i = 0; i < objectsToActivate.Length; i++) objectsToActivate[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoTrigger()
    {
        // play animations
        //if (animToPlay != null) animToPlay.Play();
        for (int i = 0; i < animationsToPlay.Length; i++) animationsToPlay[i].Play();

        // activate objects
        for (int i = 0; i < objectsToActivate.Length; i++) objectsToActivate[i].SetActive(true);

        // deactivate objects
        for (int i = 0; i < objectsToDeactivate.Length; i++) objectsToDeactivate[i].SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered!");
        DoTrigger();

        flame.SetTrigger("Fizzle");

    }
}
