using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSFX : MonoBehaviour
{
    [SerializeField] AudioClip sfxClip;
    public void NoiseSFX()
    {
        GetComponent<AudioSource>().PlayOneShot(sfxClip);
    }
}
