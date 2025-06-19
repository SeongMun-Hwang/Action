using UnityEngine;

public class IdleState : IState
{
    private Animator animator;

    public IdleState(Animator animator)
    {
        this.animator = animator;
    }
    public void Enter()
    {
        Debug.Log("IdleState Enter");
        animator.SetTrigger("Idle_Trigger");
    }
    public  void Update()
    {
    }
    public  void Exit()
    {
        Debug.Log("IdleState Exit");
    }
}
