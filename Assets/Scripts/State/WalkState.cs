using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : IState
{
    private Animator animator;
    private AnimatorController animatorController;
    private bool isRunningDefault = false;
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (animatorController.moveSpeed == PlayerStats.walkSpeed)
            {
                isRunningDefault = true;
                animatorController.moveSpeed = PlayerStats.runSpeed;
                animator.SetFloat("moveSpeed", PlayerStats.runSpeed);
            }
            else
            {
                isRunningDefault = false;
                animatorController.moveSpeed = PlayerStats.walkSpeed;
                animator.SetFloat("moveSpeed", PlayerStats.walkSpeed);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetFloat("moveSpeed", PlayerStats.sprintSpeed);
            animatorController.moveSpeed = PlayerStats.sprintSpeed;
            Debug.Log("running");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isRunningDefault)
            {
                animatorController.moveSpeed = PlayerStats.runSpeed;
            }
            else
            {
                animatorController.moveSpeed = PlayerStats.walkSpeed;
            }
            animator.SetFloat("moveSpeed", animatorController.moveSpeed);
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
