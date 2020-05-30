using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{

    private int randomSpotInt;

    private float speed;

    private Transform[] moveSpotsTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = EnemyAI.Instance.enemyMoveSpeed;
        moveSpotsTransform = EnemyAI.Instance.moveSpotsTransform;

        randomSpotInt = Random.Range(0, moveSpotsTransform.Length);
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     if(Vector2.Distance(animator.transform.position, moveSpotsTransform[randomSpotInt].position) > .02f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position,
                moveSpotsTransform[randomSpotInt].position, speed * Time.deltaTime);
        }
        else
        {
            randomSpotInt = Random.Range(0, moveSpotsTransform.Length);
        }
    }
}
