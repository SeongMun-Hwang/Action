using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private int combo = 0;
    private Coroutine attackDelayCoroutine;
    private float attackDelay = 0.5f;
    private float timer = 0f;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttack();
    }
    private void HandleAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack_Trigger");
            animator.SetInteger("Combo", combo);
            if(attackDelayCoroutine != null)
            {
                StopCoroutine(attackDelayCoroutine);
            }
        }
    }
    private void AttackDelayTimer()
    {
        attackDelayCoroutine = StartCoroutine(AttackDelayCoroutine());
    }
    private IEnumerator AttackDelayCoroutine()
    {
        while (timer < attackDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        combo = 0;
        animator.SetInteger("Combo", combo);
    }
}
