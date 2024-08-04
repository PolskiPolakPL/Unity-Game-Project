using UnityEngine;

public class PickableObjectsScript : MonoBehaviour
{
    [SerializeField] string pickableTag;
    [Header("- - - = = Ray Casting = = - - -")]
    [SerializeField] Camera cam;
    [Tooltip("Layers that will collide with the moveable object.")]
    [SerializeField] LayerMask collisionLayers;
    [Tooltip("Maximum distance that catched object can be from camera.")]
    [SerializeField] float maxDistance;

    private Transform pickableObjT;
    private int pickableObjLayer;
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        if(cam==null)
            cam = Camera.main;
        foreach(Transform child in transform)
        {
            child.gameObject.tag = pickableTag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            CatchMoveableObject();
        }
        if(Input.GetMouseButton(0) && pickableObjT!=null)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            pickableObjT.gameObject.layer = 2;
            if(Physics.Raycast(ray, out hit, maxDistance, collisionLayers))
            {
                pickableObjT.position = hit.point;
            }
            else
            {
                pickableObjT.position = cam.transform.position + cam.transform.forward * maxDistance;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(pickableObjT!=null)
                pickableObjT.gameObject.layer = pickableObjLayer;
            pickableObjT = null;
        }
    }

    void CatchMoveableObject()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag.ToLower() == pickableTag.ToLower())
        {
            pickableObjT = hit.transform;
            pickableObjLayer = pickableObjT.gameObject.layer;
        }
        else
            pickableObjT = null;
    }
}
