using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavoir : StateMachineBehaviour
{
    private Transform playerTransform;

     float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = EnemyAI.Instance.enemyMoveSpeed;
        playerTransform = EnemyAI.Instance.playerTransform;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 enemyTransform = animator.transform.position;

        animator.transform.position = Vector2.MoveTowards(enemyTransform, playerTransform.position, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isFollowing", false);
        }
    }
}
