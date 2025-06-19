using UnityEngine;

public class WalkState : JumpState
{
    private Animator animator;
    public WalkState(Animator animator, CharacterController characterController)
        : base(animator, characterController)
    {
    }
    public void Enter()
    {
        animator.SetTrigger("Walk_Trigger");
    }
    public void Update()
    {
        base.Update();
    }
    public void Exit()
    {

    }
}
