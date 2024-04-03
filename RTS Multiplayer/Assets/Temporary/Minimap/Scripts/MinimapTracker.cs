using UnityEngine;

public class MinimapTracker : MonoBehaviour
{
    public MinimapObjectCreator Creator;
    [Tooltip("Child Object that is being tracked")]public Transform Target;
    public float HeightOffset = 10;
    [SerializeField] bool IsTrackingPosition;
    [SerializeField] bool IsTrackingRotation;
    // Start is called before the first frame update
    void Start()
    {
        TrackPosition();
        if(IsTrackingRotation)
            TrackRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsTrackingPosition)
            TrackPosition();
        if (IsTrackingRotation)
            TrackRotation();
    }

    public void TrackPosition()
    {
        if (!isTargetNull())
        {
            float y = Target.position.y + HeightOffset;
            transform.position = new Vector3(Target.position.x, y, Target.position.z);
        }
        else return;
        
    }
    public void TrackRotation()
    {
        if (!isTargetNull())
        {
            transform.rotation = Target.rotation;
        }
        else return;
    }

    bool isTargetNull()
    {
        if(Target == null)
        {
            Creator.MoveToEmptyTrackersList(this);
            return true;
        }
        else return false;
    }
}
