using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float maxVerticalAngle = 60f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 0.1f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private float _verticalRotation = 0f;
    private bool _isGrounded;
    private Rigidbody _rigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rigidbody = GetComponent<Rigidbody>();

        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }
        _rigidbody.freezeRotation = true;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    void HandleMovement()
    {
        float moveForward = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float moveSide = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        Vector3 newPosition = _rigidbody.position + move;
        _rigidbody.MovePosition(newPosition);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        transform.Rotate(0, mouseX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }

    void HandleJump()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
