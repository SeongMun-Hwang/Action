using UnityEngine;

public class IdleState : JumpState
{
    private Animator animator;
    public IdleState(Animator animator, CharacterController characterController)
        : base(animator, characterController)
    {

    }
    public void  Enter()
    {
        animator.SetTrigger("Idle_Trigger");
    }
    public void Update()
    {
        base.Update();
    }
    public void Exit()
    {

    }
}
