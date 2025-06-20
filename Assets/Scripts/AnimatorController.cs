using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    //private
    private Animator animator;
    private CharacterController characterController;
    private Vector2 smoothInput = Vector2.zero;
    private float smoothSpeed = 5f;
    //public
    public PlayerStats playerStats = new PlayerStats();
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
        HandleMovement();
        currentState?.Update();
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
    private void ChangeState(IState newState)
    {
        if (currentState?.GetType() == newState.GetType()) return;
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }
}
