using UnityEngine;
using UnityEngine.AI;

/*****************************************************
 * Filename:            CEnemyAttackState.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Concrete attack state
 *****************************************************/

public class CEnemyAttackState : CEnemyBaseState
{
    /*
    * player:           Reference to the player for pathfinding and attack logic
    * enemyNavAgent:    Reference to the enemy NavMeshAgent for NavMesh control
    * enemyAnimator:    Reference to the enemy Animator for triggering animations
    * enemyHP:          Reference to the enemy's health script
    */
    private Transform player;
    private NavMeshAgent enemyNavAgent;
    private Animator enemyAnimator;
    private HealthController enemyHP;

    /***************************************
     * EnterState():    Called whenever an enemy changes states, this function is called
     *                  and establishes necessary references and linkages for state functionality
     *   
     * Takes:           CEnemyStateManager, GameObject
     *                  
     * Date:            11/01/2021
     * Mod date:        11/08/2021
     * Mod initials:    TCC
    **************************************/
    public override void EnterState(CEnemyStateManager enemyState, GameObject enemyObj)
    {
        player = GameManager.gmInstance.player.transform;
        enemyNavAgent = enemyObj.GetComponent<NavMeshAgent>();
        enemyAnimator = enemyObj.GetComponentInChildren<Animator>();
        enemyHP = enemyObj.GetComponent<HealthController>();
    }

    /***************************************
     * UpdateState():   Called within Unity's Update() function, handles all of a given state's 
     *                  functionality and updates
     *   
     * Takes:           CEnemyStateManager, GameObject
     *                  
     * Date:            11/01/2021
     * Mod date:        11/08/2021
     * Mod initials:    TCC
    **************************************/
    public override void UpdateState(CEnemyStateManager enemyState, GameObject enemyObj)
    {
        distanceToPlayer = Vector3.Distance(player.position, enemyNavAgent.transform.position);
        FacePlayer();

        if (enemyHP.currentHealth <= 0)
        {
            enemyState.ChangeState(enemyState.dead);
        }

        if (!enemyAttacking && distanceToPlayer < projectileRadius)
        {
            FacePlayer();
            enemyAttacking = true;
            enemyAnimator.SetBool("b_TargetInRange", true); 
        }

        else if (distanceToPlayer >= projectileRadius)
        {
            enemyAttacking = false;
            enemyAnimator.SetBool("b_TargetInRange", false);
            enemyState.ChangeState(enemyState.pursue);
        }
    }

    /***************************************
    * FacePlayer()     Makes sure enemy is facing player at all times once player is detected
    *
    *
    * 
    * Mod date:        08/15/2021
    * Mod initials:    TCC
    **************************************/
void FacePlayer()
    {
        Vector3 tDirection = (player.position - enemyNavAgent.transform.position).normalized; // Get the player's direction
        Quaternion qLookRotation = Quaternion.LookRotation(new Vector3(tDirection.x, 0, tDirection.z)); // Calculates the rotation needed to face player
        enemyNavAgent.transform.rotation = Quaternion.Slerp(enemyNavAgent.transform.rotation, qLookRotation, Time.deltaTime * 15f); // Performs the rotation with interpolation
    }
}
