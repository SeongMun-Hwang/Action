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
    private StateMachine stateMachine;
    private IdleState idleState;
    private WalkState walkState;
    private LightAttackState lightAttackState;
    //상태 접근
    public IdleState IdleState => idleState;
    public WalkState WalkState => walkState;
    public LightAttackState LightAttackState => lightAttackState;

    public Animator Animator => animator;
    public CharacterController Controller => characterController;
    public StateMachine StateMachine => stateMachine;

    //combo
    public bool isComboEnable;
    public bool isNextCombo;
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new StateMachine();
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        lightAttackState = new LightAttackState(this);

        stateMachine.ChangeState(idleState);
    }

    void Update()
    {
        HandleDefaultMovement();
        HandleAttack();
        stateMachine.Update();
        HandleJump();
        HandleMovement();
    }
    private void HandleMovement()
    {
        if(stateMachine.Current is LightAttackState)
        {
            // 공격 상태에서는 이동을 하지 않음
            return;
        }
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
            stateMachine.ChangeState(WalkState);
        }
        else if (move.magnitude <= 0.1f && !(stateMachine.Current is LightAttackState))
        {
            stateMachine.ChangeState(idleState);
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
    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(lightAttackState);
        }
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
    /*
     * 콤보 상태 관리 함수
     */
    public void Combo_Enable()
    {
        isComboEnable = true;
    }
    public void Combo_Disable()
    {
        isComboEnable = false;
    }
    public void AttackToIdle()
    {
        if(isNextCombo)
        {
            animator.SetTrigger("NextCombo_Trigger");
        }
        else
        {
            stateMachine.ChangeState(idleState);
        }
        isNextCombo = false;
    }
}
