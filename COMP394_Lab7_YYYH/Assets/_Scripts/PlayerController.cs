using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;

    [Header("Movements")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Vector3 velocity;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.5f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset downward velocity to keep the player grounded
        }

        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("jumping");
            velocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"Player touch {other.gameObject.name}");
        if(other.gameObject.name != "PlatformReveal")
        this.transform.parent = other.transform;
    }

    private void OnCollisionExit(Collision other)
    {
        this.transform.parent = null;
    }

}
