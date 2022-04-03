using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject player;
    public bool shed;
    public GameObject ghostOwner;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = teleportTarget.transform.position;

        if (shed) {
            ghostOwner.SetActive(true);
            ghostOwner.GetComponent<Animator>().SetTrigger("Start");
        }
    }
}
