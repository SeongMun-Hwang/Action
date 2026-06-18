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
        // Note: HashTrigger names should be added to PlayerController if needed, 
        // but for now we'll use a local hash or string if not available.
        // Let's assume we want to be consistent and add Idle_Trigger to PlayerController.
        player.Animator.SetTrigger(Animator.StringToHash("Idle_Trigger"));
        player.Animator.SetFloat(PlayerController.Hash_MoveSpeed, 0f);
    }
    public  void Update()
    {
    }
    public  void Exit()
    {
        Debug.Log("IdleState Exit");
    }
}
