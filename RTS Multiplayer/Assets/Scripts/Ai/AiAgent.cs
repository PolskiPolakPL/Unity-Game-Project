using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgent : MonoBehaviour
{
    [HideInInspector] public AiSensor scanner;
    public AiStateMachine moveStateMachine;
    public AiStateMachine fightStateMachine;
    public AiStateMachine suppressionStateMachine;
    public AiStateId moveInitialState;
    public AiStateId fightInitialState;
    public AiStateId suppressionInitialState;


    // Start is called before the first frame update
    void Start()
    {
        moveStateMachine = new AiStateMachine(this);
        moveStateMachine.SetState(moveInitialState);
        moveStateMachine.RegisterState(new AiStandingState());
        moveStateMachine.RegisterState(new AiMovingState());

        fightStateMachine = new AiStateMachine(this);
        fightStateMachine.SetState(fightInitialState);
        fightStateMachine.RegisterState(new AiIdleState());
        fightStateMachine.RegisterState (new AiAimingState());
        fightStateMachine.RegisterState(new AiFightingState());

        suppressionStateMachine = new AiStateMachine(this);
        suppressionStateMachine.SetState(suppressionInitialState);
        suppressionStateMachine.RegisterState(new AiNotSuppressedState());
        suppressionStateMachine.RegisterState(new AiSuppressedState());
        suppressionStateMachine.RegisterState(new AiPinnedState());

        scanner = GetComponent<AiSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
