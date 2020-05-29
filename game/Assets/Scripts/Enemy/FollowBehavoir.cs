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

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
