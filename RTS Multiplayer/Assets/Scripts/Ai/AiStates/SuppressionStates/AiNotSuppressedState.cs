using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiNotSuppressedState : AiState
{
    public void Enter(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public AiStateId GetId()
    {
        return AiStateId.NotSuppressed;
    }

    public void Update(AiAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
