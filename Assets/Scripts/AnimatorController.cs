using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    //private
    private Animator animator;
    private CharacterController characterController;
    private Vector3 velocity;
    private State currentState;
    private Vector2 smoothInput = Vector2.zero;
    private float smoothSpeed = 5f;
    //public
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentState = State.Idle;
    }

    void Update()
    {
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

        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = -2f;
        }

        if(move.magnitude > 0.1f)
        {
            SetState(State.Walking);
        }
        else
        {
            SetState(State.Idle);
        }
    }
    private void SetState(State newState)
    {
        if (currentState == newState) return;
        else currentState = newState;

        switch (currentState)
        {
            case State.Idle:
                animator.SetTrigger("Idle_Trigger");
                break;
            case State.Walking:
                animator.SetTrigger("Walk_Trigger");
                break;
        }
    }
}
