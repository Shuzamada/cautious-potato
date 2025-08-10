using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementControler : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;

    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] public Vector3 moveDirection = Vector3.zero;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 5f;

    [Header("Camera")]
    [SerializeField] public float mouseSensitivity = 2f;
    [SerializeField] public Transform cameraTransform; // ������ �� ������
    [SerializeField] public float maxLookAngle = 80f; // ����������� ������� �����/����

    private float verticalRotation = 0f; // ������ ��� ������
    private bool onGround = false;
    private float verticalVelocity = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInputAction = new PlayerInputAction();

        // �������� ��������
        playerInputAction.Player.Jump.performed += OnJumpPerformed;
        playerInputAction.Player.Jump.canceled += OnJumpCanceled;
        playerInputAction.Player.Movement.performed += OnMovementPerformed;
        playerInputAction.Player.Movement.canceled += OnMovementCanceled;
        playerInputAction.Player.Camera.performed += OnLookPerformed;
        playerInputAction.Player.Camera.canceled += OnLookCanceled;

        // ������������� ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        if (playerInputAction == null)
        {
            playerInputAction = new PlayerInputAction();
        }


        playerInputAction.Enable();
    }


    void OnDisable()
    {
        playerInputAction.Disable();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {

        Vector3 forward = transform.forward * moveInput.y;
        Vector3 right = transform.right * moveInput.x;

        moveDirection = (forward + right).normalized;
        moveDirection.y = verticalVelocity * jumpForce;

        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }   
    }

    private void HandleRotation()
    {
        if (lookInput != Vector2.zero)
        {
            // �������������� ������� - ������� ���������
            float horizontalRotation = lookInput.x * mouseSensitivity * Time.deltaTime;
            transform.Rotate(0, horizontalRotation, 0);

            // ������������ ������� - ������� ������ ������
            verticalRotation -= lookInput.y * mouseSensitivity * Time.deltaTime;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);

            if (cameraTransform != null)
            {
                cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            }
        }
    }

    // ������ ��������� �����
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        verticalVelocity = 0.2f;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        verticalVelocity = 0f;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        lookInput = Vector2.zero;
    }

    private void MovementHandler()
    {
        Debug.Log($"Movement input: {moveInput}");
    }
}
