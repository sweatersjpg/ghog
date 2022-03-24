using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhogAnimator : MonoBehaviour
{
    SpriteRenderer sr;
    Camera cam;

    GhogController player;

    SpriteRenderer barkSpr;

    public Sprite[] running;
    public Sprite[] idle;
    public Sprite[] jump;

    public Sprite[] barkLines;

    float nextFrame;
    public float animationSpeed; // in millis (must divide by 1000 in use)

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        cam = Camera.main;

        player = GetComponent<GhogController>();

        barkSpr = transform.Find("Bark").GetComponentInChildren<SpriteRenderer>();
        // access 'Bark' obj w/ barkSpr.transform.parent.gameObject

        barkSpr.transform.parent.gameObject.SetActive(false);
    }

    int frame = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextFrame) return;
        nextFrame = Time.time + animationSpeed / 1000;

        frame++;

        DogAnimation();

        if (Time.time < barkEnd)
        {
            barkSpr.sprite = barkLines[frame % barkLines.Length];

            float dir = player.velocity.x / Mathf.Abs(player.velocity.x);
            barkSpr.transform.parent.localScale = new Vector3(dir, barkSpr.transform.parent.localScale.y, barkSpr.transform.parent.localScale.z) * 1.1f;
        }
        else if (barkSpr.transform.parent.gameObject.activeSelf) barkSpr.transform.parent.gameObject.SetActive(false);
    }

    void DogAnimation()
    {
        Vector2 vel = new Vector2(player.velocity.x, player.velocity.z);

        bool vflip = true;
        if (transform.position.x < cam.transform.position.x) vflip = false;

        if (vel.x > 2) sr.flipX = false;
        else if (vel.x < -2) sr.flipX = true;
        else if (vel.y > 1) sr.flipX = vflip;
        else if (vel.y < -1) sr.flipX = !vflip;

        if(!player.isGrounded)
        {
            if (player.velocity.y > player.jumpForce/2) sr.sprite = jump[1];
            else if (player.velocity.y > -3) sr.sprite = jump[2];
            else sr.sprite = jump[4];
        }
        else if (vel.magnitude > 2f)
        {
            sr.sprite = running[frame % running.Length];
        } else
        {
            sr.sprite = idle[frame %= idle.Length];
        }
    }

    float barkEnd;

    public void StartBark()
    {
        barkSpr.transform.parent.gameObject.SetActive(true);

        float dir = player.velocity.x / Mathf.Abs(player.velocity.x);
        barkSpr.transform.parent.localScale = new Vector3(dir, 1, 1);

        barkEnd = Time.time + (3 * animationSpeed)/1000;
    }
}
