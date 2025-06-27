using UnityEngine;

public class EvadeState : IState
{
    private PlayerController player;
    public EvadeState(PlayerController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        Debug.Log("EvadeState Enter");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("EvadeState Exit");
    }
}