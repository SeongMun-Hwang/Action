using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : IState
{
    private Animator animator;
    private AnimatorController animatorController;
    public float acceleration = 2f;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetFloat("moveSpeed", animatorController.playerStats.runSpeed);
            animatorController.moveSpeed = animatorController.playerStats.runSpeed;
            Debug.Log("running");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetFloat("moveSpeed", animatorController.playerStats.walkSpeed);
            animatorController.moveSpeed = animatorController.playerStats.walkSpeed;
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
