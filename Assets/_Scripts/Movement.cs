using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;

    //Components
    PlayerController playerController;
    Rigidbody rb;
    Animator playerAnimator;

    //Movement references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public readonly int movementXHash = Animator.StringToHash("MoveX");
    public readonly int movementYHash = Animator.StringToHash("MoveY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isJumping) return;

        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    public void OnMovement(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
        playerAnimator.SetFloat(movementXHash, inputVector.x);
        playerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    public void OnJump(InputValue inputValue)
    {
        playerController.isJumping = inputValue.isPressed;
        rb.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        playerAnimator.SetBool(isJumpingHash, playerController.isJumping);
    }

    public void OnRun(InputValue inputValue)
    {
        playerController.isRunning = inputValue.isPressed;

        playerAnimator.SetBool(isRunningHash, playerController.isRunning);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, false);
    }
}
