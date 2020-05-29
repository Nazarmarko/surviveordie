using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public float attackRange;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public int damage;

    public GameObject attackRangeobj;

    //public Transform attackRangeObj;

    void Update()
    {
        if (timeBtwAttack <= 0.3)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                 //   enemiesToDamage[i].GetComponent<EnemyAI>().health -= damage;
                }

            }

            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //void ChangeAttackPos()
    //{
    //    attackRangeobj.transform.position = new Vector3(-5, -1.57f, 0);
    //    Debug.Log("Change");
    //}

    //void ChangeAttackPosBack()
    //{
    //    attackRangeobj.transform.position = new Vector3(3, -1.57f, 0);
    //    Debug.Log("ChangeBack");
    //}

}
