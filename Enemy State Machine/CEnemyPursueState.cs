using UnityEngine;
using UnityEngine.AI;

/*****************************************************
 * Filename:            CEnemyPursueState.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Concrete pursuit state 
 *****************************************************/
public class CEnemyPursueState : CEnemyBaseState
{
    /*****************************************************
     * mc_Player:        Reference to the player for pathfinding and attack logic
     * mc_EnemyNav:      Reference to the enemy NavMeshAgent for NavMesh control
     * mc_EnemyAnim:     Reference to the enemy Animator for triggering animations
     * mc_EnemyHealth:   Reference to the enemy's CHealth script
    *****************************************************/
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
        // Check position and pursue player
        distanceToPlayer = Vector3.Distance(player.position, enemyNavAgent.transform.position);
        enemyNavAgent.SetDestination(player.position);    
        enemyAnimator.SetBool("isWalking", true);

        if (enemyHP.currentHealth <= 0)
        {
            enemyState.ChangeState(enemyState.dead);
        }

        else if (distanceToPlayer <= projectileRadius)
        {
            enemyAnimator.SetBool("isWalking", false);
            enemyState.ChangeState(enemyState.attack);
        }        
    }
}
