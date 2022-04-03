using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GhogController : MonoBehaviour
{
    [SerializeField] AudioSource dogAudioSource;
    [SerializeField] AudioClip[] dogBarkSFX;
    [SerializeField] AudioClip dogFootstep,
        dogJumpSFX;

    Rigidbody rb;
    public Vector3 velocity;

    //GameObject mainCamera;

    public float gravity = -0.05f;
    public float deceleration = 0.9f;
    public float acceleration = 0.5f;
    public float maxSpeed = 1;
    public float jumpForce = 5;

    public bool isGrounded = false;

    CameraController cameraController;

    bool up;
    bool down;
    bool left;
    bool right;

    // Start is called before the first frame update
    void Start()
    {
        dogAudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        cameraController = Camera.main.GetComponent<CameraController>();

        velocity = new Vector3(0, 1, 0);
    }

    int jumpPressed = 0;
    int jumpReleased = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) jumpPressed = 5;
        if (Input.GetButtonUp("Jump")) jumpReleased = 5;

        left = Input.GetKey("a");
        right = Input.GetKey("d");
        up = Input.GetKey("w");
        down = Input.GetKey("s");

        if (Input.GetKeyDown("e"))
        {
            dogAudioSource.clip = dogBarkSFX[Random.Range(0, dogBarkSFX.Length)];
            dogAudioSource.Play();

            // trigger nearby objects
            TelemetryLogger.Log(this, "Bark", transform.position);
            GetComponent<GhogAnimator>().StartBark();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Collider[] hits = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), 0.4f /*, LayerMask.GetMask("Environment")*/);
        bool touchingGround = false;
        foreach (Collider hit in hits) if (hit.gameObject.layer == LayerMask.NameToLayer("Walkable")) touchingGround = true;

        if (touchingGround)
        {
            isGrounded = true;
            if (velocity.y < 0) velocity.y = 0;
        }

        if (velocity.y <= gravity * 16) isGrounded = false;

        if (jumpPressed > 0 && isGrounded)
        {
            velocity.y = jumpForce;
            jumpPressed = 0;
            isGrounded = false;

            dogAudioSource.PlayOneShot(dogJumpSFX);
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

        if (left) velocity.x -= acc;
        if (right) velocity.x += acc;
        if (up) velocity.z += acc;
        if (down) velocity.z -= acc;

        velocity.y += gravity;
        if (velocity.y < 0) velocity.y += gravity;

        Vector3 limitedVel = new Vector2(velocity.x, velocity.z);
        limitedVel = Vector2.ClampMagnitude(limitedVel, maxSpeed);
        velocity.x = limitedVel.x;
        velocity.z = limitedVel.y;

        velocity.x *= deceleration;
        velocity.z *= deceleration;

        rb.AddForce(velocity - rb.velocity, ForceMode.VelocityChange);

        if (isGrounded && (velocity.x > 0.95f || velocity.x < -0.95f || velocity.z > 0.95f || velocity.z < -0.95f))
        {
            if (!dogAudioSource.isPlaying)
                dogAudioSource.PlayOneShot(dogFootstep, 0.5f);
        }
    }

    Transform currentRoom;
    [SerializeField] GameObject[] lights;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "roomLighting")
        {
            currentRoom = other.transform;
            GameObject currentLightRoom = currentRoom.Find("RoomLight").gameObject;

            Vector3 cameraPos = currentRoom.Find("CameraPosition").position;
            cameraController.MoveTo(cameraPos);

            currentLightRoom.SetActive(true);
            lights.Except(new GameObject[] { currentLightRoom }).ToList().ForEach(g => g.SetActive(false));

            TelemetryLogger.Log(this, "Room: Enter", currentRoom.name);
        }
    }
}
