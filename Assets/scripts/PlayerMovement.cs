
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed = 7f;
    public float laneSpeed = 5f;
    public float laneLimit = 4f;

    public float jumpHeight = 2f;
    public float gravity = -20f;

    public float slideHeight = 1f;
    public float slideDuration = 1f;
    public float slideVisualScale = 0.65f;

    public Transform characterModel;

    private CharacterController controller;
    private float verticalVelocity;

    private float normalHeight;
    private Vector3 normalCenter;
    private Vector3 normalModelScale;

    private bool isSliding;
    private float slideTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        normalHeight = controller.height;
        normalCenter = controller.center;

        if (characterModel != null)
            normalModelScale = characterModel.localScale;
    }

    void Update()
    {
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontal = 1f;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        // Jump with Up Arrow
        if (controller.isGrounded &&
            Input.GetKeyDown(KeyCode.UpArrow) &&
            !isSliding)
        {
            verticalVelocity =
                Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Slide with Down Arrow
        if (controller.isGrounded &&
            Input.GetKeyDown(KeyCode.DownArrow) &&
            !isSliding)
        {
            StartSliding();
        }

        // Slide duration
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0f)
                StopSliding();
        }

        // Gravity
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 movement = new Vector3(
            horizontal * laneSpeed,
            verticalVelocity,
            forwardSpeed
        );

        controller.Move(movement * Time.deltaTime);

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(
            position.x, -laneLimit, laneLimit
        );

        transform.position = position;
    }

    void StartSliding()
    {
        isSliding = true;
        slideTimer = slideDuration;

        controller.height = slideHeight;
        controller.center = new Vector3(
            normalCenter.x,
            slideHeight / 2f,
            normalCenter.z
        );

        if (characterModel != null)
        {
            characterModel.localScale = new Vector3(
                normalModelScale.x,
                normalModelScale.y * slideVisualScale,
                normalModelScale.z
            );
        }
    }

    void StopSliding()
    {
        isSliding = false;

        controller.height = normalHeight;
        controller.center = normalCenter;

        if (characterModel != null)
            characterModel.localScale = normalModelScale;
    }

    void OnDisable()
    {
        if (controller != null)
        {
            controller.height = normalHeight;
            controller.center = normalCenter;
        }

        if (characterModel != null)
            characterModel.localScale = normalModelScale;
    }
}
