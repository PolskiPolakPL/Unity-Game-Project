using UnityEngine;

public class MouseInputStateManager : MonoBehaviour
{
    //FSM Classes
    MouseInputBaseState _CurrentState;
    public DefaultMouseInputState DefaultState = new DefaultMouseInputState();
    public BuildingMouseInputState BuildingState = new BuildingMouseInputState();


    [Header("Unit Selection Scripts")]
    public UnitClick UnitClick;
    public UnitDrag UnitDrag;

    [Header("Unity Camera References")]
    public Camera Cam;
    public CameraScript CameraScript;
    public RaycastHit Hit;

    private void Start()
    {
        if (Cam == null)
            Cam = Camera.main;
        _CurrentState = DefaultState;
        _CurrentState.EnterState(this);
    }
    private void Update()
    {
        _CurrentState.UpdateState(this);
    }

    public void SwitchState(MouseInputBaseState state)
    {
        _CurrentState = state;
        state.EnterState(this);
    }
}