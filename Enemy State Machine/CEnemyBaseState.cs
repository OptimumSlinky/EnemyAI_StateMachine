using UnityEngine;

/*****************************************************
 * Filename:            CEnemyBaseState.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Abstract base class for boss AI state machine implementation
*****************************************************/

public abstract class CEnemyBaseState
{
    /*****************************************************
    * mf_LookRadius:            Enemy's general awareness zone
    * mf_ProjectileRadius:      Radius in which enemy can attack the player with projectiles
    * mf_AttackRadius:          Melee attack range; not applicable to all enemies
    * mf_BackoffRadius:         Distance enemy will stop from the player
    * mf_DistanceToPlayer:      Reference variable to store calculated distance between enemy and player
    * 
    * mb_IsAlive: Boolean for tracking enemy's life state
    * mb_IsAlerted: Boolean for tracking enemy's alertedness
    * mb_IsAttacking: Boolean for controlling attack state and animation events
    *****************************************************/
    public float lookRadius = 20f;
    public float projectileRadius = 10f;
    public float meleeRadius = 3f;
    public float backOffRadius = 2f;
    protected float distanceToPlayer;

    public bool enemyAlive = true;
    public bool enemyAlerted = false;
    public bool enemyAttacking = false;

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
    public abstract void EnterState(CEnemyStateManager enemyState, GameObject enemyObj);

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
    public abstract void UpdateState(CEnemyStateManager enemyState, GameObject enemyObj);
}
