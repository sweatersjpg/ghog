using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyPress : MonoBehaviour
{
    SpriteRenderer sr;
    Sprite upSprite;
    public Sprite downSprite;

    public string key;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        upSprite = sr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key)) sr.sprite = downSprite;
        else sr.sprite = upSprite;
    }
}
