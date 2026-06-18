using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // private
    private Animator animator;
    private CharacterController characterController;
    private Vector2 smoothInput = Vector2.zero;
    private float smoothSpeed = 5f;

    // Animator Hashes
    private static readonly int HashMoveSpeed = Animator.StringToHash("moveSpeed");
    private static readonly int HashWalkForward = Animator.StringToHash("Walk_Forward");
    private static readonly int HashWalkRight = Animator.StringToHash("Walk_Right");
    private static readonly int HashJumpTrigger = Animator.StringToHash("Jump_Trigger");
    private static readonly int HashIsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int HashEvadeTrigger = Animator.StringToHash("Evade_Trigger");
    private static readonly int HashIsGuardEnable = Animator.StringToHash("isGuardEnable");
    private static readonly int HashNextComboTrigger = Animator.StringToHash("NextCombo_Trigger");

    // Public properties for Hashes
    public static int Hash_MoveSpeed => HashMoveSpeed;

    // jump variables
    private bool isGrounded = true;
    private float jumpForce = 5f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    
    // default movement
    public bool isRunningDefault = true;
    
    // public
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    
    // state 선언
    private StateMachine stateMachine;
    private IdleState idleState;
    private WalkState walkState;
    private LightAttackState lightAttackState;
    private EvadeState evadeState;
    
    // 상태 접근
    public IdleState IdleState => idleState;
    public WalkState WalkState => walkState;
    public LightAttackState LightAttackState => lightAttackState;
    public EvadeState EvadeState => evadeState;

    public Animator Animator => animator;
    public CharacterController Controller => characterController;
    public StateMachine StateMachine => stateMachine;

    // combo
    public bool isComboEnable;
    public bool isNextCombo;
    
    // guard
    public bool isGuardEnable;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new StateMachine();
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        lightAttackState = new LightAttackState(this);
        evadeState = new EvadeState(this);

        stateMachine.ChangeState(idleState);
    }

    void Update()
    {
        HandleDefaultMovement();
        HandleGuard();
        HandleEvade();

        HandleAttack();
        stateMachine.Update();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (stateMachine.Current is LightAttackState)
        {
            return;
        }

        // 1. 입력 받기
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(inputX, 0, inputY);
        float inputMagnitude = Mathf.Clamp01(inputDir.magnitude);

        if (inputMagnitude > 0.1f)
        {
            // 2. 카메라 기준 이동 방향 계산
            Transform cameraTransform = Camera.main.transform;
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;

            Vector3 moveDirection = (cameraForward * inputY + cameraRight * inputX).normalized;

            // 3. 캐릭터 회전 및 이동
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            characterController.Move(moveDirection * moveSpeed * inputMagnitude * Time.deltaTime);

            // 4. 애니메이션: 캐릭터 로컬 기준으로 Forward, Right 계산 (확장성 확보)
            Vector3 localMove = transform.InverseTransformDirection(moveDirection) * inputMagnitude;

            animator.SetFloat(HashMoveSpeed, moveSpeed, 0.1f, Time.deltaTime);
            animator.SetFloat(HashWalkForward, localMove.z, 0.1f, Time.deltaTime);
            animator.SetFloat(HashWalkRight, localMove.x, 0.1f, Time.deltaTime);

            stateMachine.ChangeState(walkState);
        }
        else
        {
            animator.SetFloat(HashMoveSpeed, 0f, 0.1f, Time.deltaTime);
            animator.SetFloat(HashWalkForward, 0f, 0.1f, Time.deltaTime);
            animator.SetFloat(HashWalkRight, 0f, 0.1f, Time.deltaTime);
            stateMachine.ChangeState(idleState);
        }
    }

    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetTrigger(HashJumpTrigger);
            isGrounded = false;
            animator.SetBool(HashIsGrounded, isGrounded);
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
        if (Input.GetKeyDown(KeyCode.LeftAlt))
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
            animator.SetFloat(HashMoveSpeed, moveSpeed);
        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(lightAttackState);
        }
    }

    private void HandleEvade()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateMachine.ChangeState(evadeState);
            animator.SetTrigger(HashEvadeTrigger);
        }
    }

    private void HandleGuard()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isGuardEnable = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isGuardEnable = false;
        }
        animator.SetBool(HashIsGuardEnable, isGuardEnable);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ground layer == 3
        if (hit.gameObject.layer == 3)
        {
            isGrounded = true;
            animator.SetBool(HashIsGrounded, isGrounded);
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
        if (isNextCombo)
        {
            animator.SetTrigger(HashNextComboTrigger);
        }
        else
        {
            stateMachine.ChangeState(idleState);
        }
        isNextCombo = false;
    }
}
