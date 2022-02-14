using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhogController : MonoBehaviour
{

    Rigidbody rb;
    public Vector3 velocity;

    public GameObject mainCamera;

    public float gravity = -0.05f;
    public float deceleration = 0.9f;
    public float acceleration = 0.5f;
    public float maxSpeed = 1;
    public float jumpForce = 5;

    public bool isGrounded = false;

    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        cameraController = mainCamera.GetComponent<CameraController>();

        velocity = new Vector3(0, 1, 0);
    }

    int jumpPressed = 0;
    int jumpReleased = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) jumpPressed = 5;
        if (Input.GetButtonUp("Jump")) jumpReleased = 5;
    }

    private void FixedUpdate()
    {

        Collider[] hits = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), 0.4f /*, LayerMask.GetMask("Environment")*/);
        bool touchingGround = false;
        foreach (Collider hit in hits) if (hit.name != "Ghog" && hit.tag != "roomLighting") touchingGround = true;

        if (touchingGround)
        {
            isGrounded = true;
            if(velocity.y < 0) velocity.y = 0;
        }

        if (velocity.y <= gravity * 16) isGrounded = false;

        if (jumpPressed > 0 && isGrounded)
        {
            velocity.y = jumpForce;
            jumpPressed = 0;
            isGrounded = false;
        }
        if (jumpPressed > 0) jumpPressed--;

        if (jumpReleased > 0 && velocity.y > 0)
        {
            velocity.y /= 2;
            jumpReleased = 0;
        }
        if (jumpReleased > 0) jumpReleased--;

        float acc = acceleration;
        if (!isGrounded) acc /= 2;
        //velocity.x += Input.GetAxis("Horizontal") * acc;
        //velocity.z += Input.GetAxis("Vertical") * acc;

        if (Input.GetKey("a")) velocity.x -= acc;
        if (Input.GetKey("d")) velocity.x += acc;
        if (Input.GetKey("w")) velocity.z += acc;
        if (Input.GetKey("s")) velocity.z -= acc;

        velocity.y += gravity;
        if (velocity.y < 0) velocity.y += gravity;
        //if (isGrounded && velocity.y < 0) velocity.y = 0;

        Vector3 limitedVel = new Vector2(velocity.x, velocity.z);
        limitedVel = Vector2.ClampMagnitude(limitedVel, maxSpeed);
        velocity.x = limitedVel.x;
        velocity.z = limitedVel.y;

        velocity.x *= deceleration;
        velocity.z *= deceleration;

        rb.AddForce(velocity - rb.velocity, ForceMode.VelocityChange);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "roomLighting")
        {
            Vector3 cameraPos = other.transform.Find("CameraPosition").position;
            cameraController.MoveTo(cameraPos);

            GameObject light = other.transform.Find("RoomLight").gameObject;
            light.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "roomLighting")
        {
            GameObject light = other.transform.Find("RoomLight").gameObject;
            light.SetActive(false);
        }

    }
}
