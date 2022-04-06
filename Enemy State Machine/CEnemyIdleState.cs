using UnityEngine;
using UnityEngine.AI;

/*****************************************************
 * Filename:            CEnemyIdleState.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Concrete idle state
 *****************************************************/
public class CEnemyIdleState : CEnemyBaseState
{
    /*****************************************************
    * mc_Player:        Reference to the player for pathfinding and attack logic
    * mc_EnemyNav:      Reference to the enemy NavMeshAgent for NavMesh control
    * mc_EnemyHealth:   Reference to the enemy's CHealth script
    *****************************************************/
    private Transform player;
    private NavMeshAgent enemyNavAgent;
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
        if (!enemyAlive || !GameManager.gmInstance.player.GetComponent<HealthController>().isAlive)
        {
            return;
        }

        distanceToPlayer = Vector3.Distance(player.transform.position, enemyNavAgent.transform.position);

        if (!enemyAlerted) // If the enemy is not alerted:
        {
            if (distanceToPlayer <= lookRadius) // If the player is within the enemy's vision sphere:
            {
                FacePlayer(); // Turn them to the player

                if (true)
                {
                    // Roar(); // Alert and roar
                }

                enemyState.ChangeState(enemyState.pursue);
            }
        }

        if (enemyHP.currentHealth <= 0)
        {
            enemyState.ChangeState(enemyState.dead);
        }
    }

    /***************************************
    * FacePlayer()         Makes sure enemy is facing player at all times once player is detected
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
