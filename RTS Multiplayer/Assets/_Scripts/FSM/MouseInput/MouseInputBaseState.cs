using UnityEngine;

public abstract class MouseInputBaseState
{
    public abstract void EnterState(MouseInputStateManager mouseInput);
    public abstract void UpdateState(MouseInputStateManager mouseInput);
}
