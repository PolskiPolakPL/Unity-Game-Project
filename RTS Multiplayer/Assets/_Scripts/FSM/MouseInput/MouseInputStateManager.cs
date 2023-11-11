using UnityEngine;

public class MouseInputStateManager : MonoBehaviour
{
    //FSM Classes
    MouseInputBaseState _CurrentState;
    public DefaultMouseInputState DefaultState = new DefaultMouseInputState();
    public BuildingMouseInputState BuildingState = new BuildingMouseInputState();

    //Unity references
    public Camera camera;
    public GameObject groundMarker;
    public LayerMask friendlyMask;
    public LayerMask groundLayerMask;
    public CameraScript CameraScript { get; private set; }
    public RaycastHit hit;
    public RectTransform boxGFX;
    public GameObject itemList;
    private void Start()
    {
        camera = Camera.main;
        CameraScript = camera.transform.parent.GetComponent<CameraScript>();
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