using UnityEngine;

public class JumpState : IState
{
    private Animator animator;
    private CharacterController characterController;

    public JumpState(Animator animator, CharacterController characterController)
    {
        this.animator = animator;
        this.characterController = characterController;
    }
    public void Enter()
    {
        Debug.Log("JumpState Enter");
        animator.SetTrigger("Jump_Trigger");
    }
    public void Update()
    {
       
    }
    public void Exit()
    {
        Debug.Log("JumpState Exit");
    }
}
