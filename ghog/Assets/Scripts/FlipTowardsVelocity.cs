using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlipTowardsVelocity : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] NavMeshAgent navMesh;

    float SignedAngleBetween(Vector3 a, Vector3 b)
    {
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(a, b)));

        float signed_angle = angle * sign;

        return signed_angle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = navMesh.velocity.normalized;
        Vector3 toCamera = (Camera.main.transform.position - transform.position).normalized;

        float angle = SignedAngleBetween(vel, toCamera);

        if (angle < -1) sr.flipX = true;
        else if (angle > 1) sr.flipX = false;
    }
}
