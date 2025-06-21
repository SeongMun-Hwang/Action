using UnityEngine;
using UnityEngine.InputSystem.XR;

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
        animator.SetFloat("moveSpeed", 0f);
    }
    public  void Update()
    {
    }
    public  void Exit()
    {
        Debug.Log("IdleState Exit");
    }
}
