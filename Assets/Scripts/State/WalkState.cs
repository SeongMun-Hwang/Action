using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : IState
{
    private Animator animator;
    private AnimatorController animatorController;
    public WalkState(Animator animator, AnimatorController animatorController)
    {
        this.animator = animator;
        this.animatorController = animatorController;
    }
    public void Enter()
    {
        Debug.Log("WalkState Enter");
        animator.SetTrigger("Walk_Trigger");
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animatorController.moveSpeed = animatorController.playerStats.runSpeed;
            animator.SetFloat("moveSpeed", animatorController.playerStats.runSpeed);
            Debug.Log("running");
        }
        else
        {
            animatorController.moveSpeed = animatorController.playerStats.walkSpeed;
            animator.SetFloat("moveSpeed", animatorController.playerStats.walkSpeed);
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
