using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Range(0, 20f)]
    public float attackRange, attackDamage, attackRate;

    private float nextAttackTime = 0f;

    public LayerMask enemyLayers;

    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
       {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetTrigger("isAttacking");
                MeleeAtack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void MeleeAtack()
    {
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) { enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage); }
    }
   private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,attackRange);
    }
}
