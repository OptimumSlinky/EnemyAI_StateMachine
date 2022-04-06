using UnityEngine;

/*****************************************************
 * Filename:            CEnemyStateManager.cs
 * Date:                11/01/2021
 * Mod. Date:           11/08/2021
 * Mod. Initials:       TCC
 * Author:              Trevor Cook
 * Purpose:             Management class for the enemy state machine
 *****************************************************/
public class CEnemyStateManager: MonoBehaviour
{
    /*****************************************************
    * mc_EnemyObject:       Reference to the enemy GameObject in Unity;
    *                       passed in to allow each state to access various
    *                       needed elements
    * 
    * mc_CurrentState:      The current state within the state machine
    * 
    * mc_Idle:        Instance of the Idle state
    * mc_Pursue:      Instance of the Pursue state
    * mc_Attack:      Instance of the Attack state
    * mc_Dead:        Instance of the Dead state
    *****************************************************/
    public GameObject enemy;
    public CEnemyBaseState currentState;

    public CEnemyIdleState idle = new CEnemyIdleState();
    public CEnemyPursueState pursue = new CEnemyPursueState();
    public CEnemyAttackState attack = new CEnemyAttackState();
    public CEnemyDeathState dead = new CEnemyDeathState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = idle;
        currentState.EnterState(this, enemy);
    }
   
    // Update is called once per frame; calls UpdateState() for the current state every frame
    void Update()
    {
        currentState.UpdateState(this, enemy);
    }

    /***************************************
     * ChangeState():   Changes the state machine's current state 
     *   
     * Takes in:        CEnemyBaseState
     *                  
     * Date:            11/01/2021
     * Mod date:        11/08/2021
     * Mod initials:    TCC
    **************************************/
    public void ChangeState(CEnemyBaseState newState)
    {
        currentState = newState;
        newState.EnterState(this, enemy);
    }
}
