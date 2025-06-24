using UnityEngine;

public class LightAttackState : IState
{
    private PlayerController player;

    public LightAttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.Animator.SetTrigger("Attack_Trigger");
    }

    public void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (player.isComboEnable)
            {
                player.isNextCombo = true;
            }
        }


    }

    public void Exit()
    {
        Debug.Log("Exit LightAttackState");
    }
}
