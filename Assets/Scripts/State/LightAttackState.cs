using UnityEngine;

public class LightAttackState : IState
{
    private Animator animator;
    private CharacterController characterController;

    private int combo = 1;
    private float timer = 0f;
    private float comboResetTime = 1f;

    public LightAttackState(Animator animator, CharacterController characterController)
    {
        this.animator = animator;
        this.characterController = characterController;
    }

    public void Enter()
    {
        Debug.Log("LightAttackState Enter");
        combo = 1;
        animator.SetInteger("ComboIndex", combo);
        animator.SetTrigger("Attack_Trigger");
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            combo++;
            if (combo > 4) combo = 1; // 최대 콤보 수 4

            animator.SetInteger("Combo", combo);
            animator.SetTrigger("Attack_Trigger");
            timer = 0f;
        }

        if (timer > comboResetTime)
        {
            combo = 0; // 콤보 리셋
        }
    }

    public void Exit()
    {
        combo = 1;
        Debug.Log("LightAttackState Exit");
    }
}
