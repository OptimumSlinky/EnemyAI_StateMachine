using UnityEngine;
using UnityEngine.AI;

/*****************************************************
 * Filename:            CEnemyDeathState.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Concrete death state
*****************************************************/
public class CEnemyDeathState : CEnemyBaseState
{
    /*****************************************************
     * mc_EnemyNav:      Reference to the enemy NavMeshAgent for NavMesh control
     * mc_EnemyAnim:     Reference to the enemy Animator for triggering animations
    *****************************************************/
    private NavMeshAgent enemyNavAgent;
    private Animator enemyAnimator;

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
        enemyAnimator = enemyObj.GetComponentInChildren<Animator>();
        enemyNavAgent = enemyObj.GetComponent<NavMeshAgent>();

        enemyAttacking = false;
        enemyAlive = false;
        enemyNavAgent.isStopped = true;
        // enemyObj.GetComponentInChildren<Canvas>().enabled = false;
        enemyObj.GetComponent<Collider>().enabled = false;
        enemyAnimator.SetTrigger("timeToDie");
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
        return;
    }
}
