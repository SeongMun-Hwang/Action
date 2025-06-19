using UnityEngine;

public class WalkState : IState
{
    private Animator animator;

    public WalkState(Animator animator)
    {
        this.animator = animator;
    }
    public void Enter()
    {
        Debug.Log("WalkState Enter");
        animator.SetTrigger("Walk_Trigger");
    }
    public void Update()
    {

    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
