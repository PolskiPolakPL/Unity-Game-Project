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
            CatchObject();
        }
        if(Input.GetMouseButton(0) && pickableObjT!=null)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            MoveObject();
        }
        if(Input.GetMouseButtonUp(0))
        {
            pickableObjT = null;
        }
    }

    void CatchObject()
    {
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.tag.ToLower() == pickableTag.ToLower())
        {
            pickableObjT = hit.transform;
        }
        else
            pickableObjT = null;
    }

    void MoveObject()
    {
        if (Physics.Raycast(ray, out hit, maxDistance, collisionLayers))
            pickableObjT.position = hit.point;
        else
            pickableObjT.position = cam.transform.position + cam.transform.forward * maxDistance;
    }
}
