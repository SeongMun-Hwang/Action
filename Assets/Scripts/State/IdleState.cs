using UnityEngine;
using UnityEngine.InputSystem.XR;

public class IdleState : IState
{
    private PlayerController player;
    public IdleState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("IdleState Enter");
        player.Animator.SetTrigger("Idle_Trigger");
        player.Animator.SetFloat("moveSpeed", 0f);
    }
    public  void Update()
    {
    }
    public  void Exit()
    {
        Debug.Log("IdleState Exit");
    }
}
