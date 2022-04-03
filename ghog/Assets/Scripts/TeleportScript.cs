using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    public bool shed;
    public GameObject ghostOwner;

    [SerializeField] AudioClip doorSFX;
    bool ghostTurnedOn;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f,1.2f);
        GetComponent<AudioSource>().PlayOneShot(doorSFX);

        player.transform.position = teleportTarget.transform.position;

        if (shed) {
            if (ghostTurnedOn)
                ghostOwner.GetComponent<Animator>().SetTrigger("Start");

            ghostOwner.SetActive(true);
            ghostTurnedOn = true;
        }
    }
}
