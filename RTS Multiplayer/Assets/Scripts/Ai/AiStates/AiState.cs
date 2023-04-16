using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    Standing,
    Moving,
    Idle,
    Aiming,
    Fighting,
    NotSuppressed,
    Suppressed,
    Pinned
}

public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
