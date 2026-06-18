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
        player.Animator.SetFloat(PlayerController.Hash_MoveSpeed, player.moveSpeed);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.moveSpeed = PlayerStats.sprintSpeed;
            player.Animator.SetFloat(PlayerController.Hash_MoveSpeed, player.moveSpeed);
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
            player.Animator.SetFloat(PlayerController.Hash_MoveSpeed, player.moveSpeed);
        }
    }
    public void Exit()
    {
        Debug.Log("WalkState Exit");
    }
}
