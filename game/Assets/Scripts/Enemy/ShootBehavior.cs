using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : StateMachineBehaviour
{
    private GameObject bullet;

    private float shootDelay, startShootTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bullet = EnemyAI.Instance.bullet;
        startShootTime = EnemyAI.Instance.startShootTime;
        shootDelay = startShootTime;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(shootDelay <= 0)
        {
            Instantiate(bullet, animator.transform.position, Quaternion.identity);
            shootDelay = startShootTime;
        }
        shootDelay -= Time.deltaTime;
    }
}
