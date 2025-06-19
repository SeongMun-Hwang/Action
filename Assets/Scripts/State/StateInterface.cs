using UnityEngine;

public interface IState {
    void Enter();
    void Update();
    void Exit();
}
public abstract class JumpState : IState
{
    private Animator animator;
    private CharacterController characterController;
    private Vector3 velocity;
    private float jumpForce = 5f;
    private float gravity = -9.81f;

    public JumpState(Animator animator, CharacterController characterController)
    {
        this.animator = animator;
        this.characterController = characterController;
    }
    public virtual void Enter()
    {

    }

    public virtual void Update()
    {
        if (characterController.isGrounded)
        {
            velocity.y = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    public virtual void Exit()
    {

    }

}