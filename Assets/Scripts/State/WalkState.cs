using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : IState
{
    private Animator animator;
    private PlayerController playerController;
    public float acceleration = 2f;
    public WalkState(Animator animator, PlayerController animatorController)
    {
        this.animator = animator;
        this.playerController = animatorController;
    }
    public void Enter()
    {
        Debug.Log("WalkState Enter");
        animator.SetFloat("moveSpeed", playerController.moveSpeed);
        animator.SetTrigger("Walk_Trigger");
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetFloat("moveSpeed", PlayerStats.sprintSpeed);
            playerController.moveSpeed = PlayerStats.sprintSpeed;
            Debug.Log("running");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (playerController.isRunningDefault)
            {
                playerController.moveSpeed = PlayerStats.runSpeed;
            }
            else
            {
                playerController.moveSpeed = PlayerStats.walkSpeed;
            }
            animator.SetFloat("moveSpeed", playerController.moveSpeed);
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
