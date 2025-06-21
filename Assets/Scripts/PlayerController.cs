using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private
    private Animator animator;
    private CharacterController characterController;
    private Vector2 smoothInput = Vector2.zero;
    private float smoothSpeed = 5f;
    //jump variables
    private bool isGrounded = true;
    private float jumpForce = 5f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    //default movement
    public bool isRunningDefault = false;

    //public
    public float moveSpeed = 5f;
    //state 선언
    private IState currentState;
    private IdleState idleState;
    private WalkState walkState;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        idleState = new IdleState(animator);
        walkState = new WalkState(animator, this);

        ChangeState(idleState);
    }

    void Update()
    {
        HandleDefaultMovement();
        currentState?.Update();
        HandleJump();
        HandleMovement();
    }
    private void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 targetInput = new Vector2(inputX, inputY);
        smoothInput = Vector2.Lerp(smoothInput, targetInput, smoothSpeed * Time.deltaTime);

        animator.SetFloat("Walk_Right", smoothInput.x);
        animator.SetFloat("Walk_Forward", smoothInput.y);

        Vector3 move = transform.right * inputX + transform.forward * inputY;
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (move.magnitude > 0.1f)
        {
            ChangeState(walkState);
        }
        else
        {
            ChangeState(idleState);
        }
    }
    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetTrigger("Jump_Trigger");
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        if (isGrounded)
        {
                velocity.y = -2f;         
        }
    }
    private void HandleDefaultMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (moveSpeed == PlayerStats.walkSpeed)
            {
                isRunningDefault = true;
                moveSpeed = PlayerStats.runSpeed;
            }
            else
            {
                isRunningDefault = false;
                moveSpeed = PlayerStats.walkSpeed;
            }
            animator.SetFloat("moveSpeed", moveSpeed);
        }
    }
    private void ChangeState(IState newState)
    {
        if (currentState == newState) return;
        
            currentState?.Exit();

        currentState = newState;
        currentState.Enter();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //ground layer == 3
        if (hit.gameObject.layer == 3)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}
