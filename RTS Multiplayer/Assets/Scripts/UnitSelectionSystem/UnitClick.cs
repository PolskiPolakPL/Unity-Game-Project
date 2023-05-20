using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    Camera cam;
    public GameObject groundMarker;
    public LayerMask friendlyMask;
    public GameObject itemList;

    RaycastHit hit;
    Ray ray;
    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click");
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, friendlyMask) || EventSystem.current.IsPointerOverGameObject()) // something with friendly mask got hit
            {
                Debug.Log("Mouse Hit");
                string hitTag = "Facility";
                try
                {
                    hitTag = hit.collider.tag;
                }
                catch(NullReferenceException) { }
                if (hitTag == "Facility" || EventSystem.current.IsPointerOverGameObject()) // building or list was hit
                {
                    Debug.Log("Facility");
                    if (InputManager.Instance.currentState != Selection.BUILDING) // and was not selected
                    {
                        UnitSelection.Instance.DeselectAll();
                        InputManager.Instance.currentState = Selection.BUILDING;
                        hit.transform.gameObject.GetComponent<BuildingScript>().ShowItemList();
                    }
                }
                else if (hitTag == "Infantry") // unit was hit
                {
                    itemList.SetActive(false);
                    InputManager.Instance.currentState = Selection.UNITS;
                    // Click & L.Shift+Click
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        // klikniêcie z Shiftem
                        UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        //klikniêcie normalne
                        UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                    }
                }
                
                
            }
            else // no hit on anything or ground
            {
                Debug.Log("No hit or ground");
                InputManager.Instance.currentState = Selection.NONE;
                itemList.SetActive(false);
                if (!Input.GetKey(KeyCode.LeftShift)) // and shift is not pressed
                {
                    UnitSelection.Instance.DeselectAll();
                    if(InputManager.Instance.currentState != Selection.BUILDING)
                        InputManager.Instance.currentState = Selection.NONE;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }
}
