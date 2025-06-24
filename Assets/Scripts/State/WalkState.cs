using UnityEngine;
using UnityEngine.InputSystem.XR;

public class WalkState : IState
{
    private PlayerController player;
    public float acceleration = 2f;
    public WalkState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("WalkState Enter");
        player.Animator.SetFloat("moveSpeed", player.moveSpeed);
        player.Animator.SetTrigger("Walk_Trigger");
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Animator.SetFloat("moveSpeed", PlayerStats.sprintSpeed);
            player.moveSpeed = PlayerStats.sprintSpeed;
            Debug.Log("running");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (player.isRunningDefault)
            {
                player.moveSpeed = PlayerStats.runSpeed;
            }
            else
            {
                player.moveSpeed = PlayerStats.walkSpeed;
            }
            player.Animator.SetFloat("moveSpeed", player.moveSpeed);
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
