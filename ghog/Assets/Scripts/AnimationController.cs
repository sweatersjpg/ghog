using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    NavMeshAgent nav;
    Rigidbody rb;
    Animator anim;
    SpriteRenderer sr;
    Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        // nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        cam = (Camera)FindObjectOfType(typeof(Camera));
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vel", new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
        anim.SetFloat("yvel", rb.velocity.y);

        Vector3 vel = rb.velocity.normalized;
        Vector3 toCamera = (cam.transform.position - transform.position).normalized;

        //vel = Quaternion.AngleAxis(-cameraPivot.transform.rotation.y, Vector3.up) * vel;

        float angle = SignedAngleBetween(vel, toCamera);

        //Debug.DrawLine(transform.position, transform.position + toCamera * 2, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + vel * 2);

        //if (vel.x > 0.1) sr.flipX = false;
        //else if (vel.x < -0.1) sr.flipX = true;

        if (angle < -1) sr.flipX = true;
        else if (angle > 1) sr.flipX = false;

        //sr.flipX = vel.x < 0;
        //Vector3 toCamera = camera.transform.position - transform.position;

        //float angle = Vector3.Angle(vel, toCamera);
        //Debug.Log(angle);
        ////if (angle > 0) sr.flipX = true;
        ////else sr.flipX = false;
    }

    float SignedAngleBetween(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(a, b)));

        float signed_angle = angle * sign;

        return signed_angle;
    }
}
