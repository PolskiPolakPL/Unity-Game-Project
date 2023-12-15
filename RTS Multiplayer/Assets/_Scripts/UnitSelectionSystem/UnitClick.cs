using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    Camera cam;
    public GameObject groundMarker;
    public LayerMask friendlyMask, groundLayerMask;
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
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, friendlyMask) || EventSystem.current.IsPointerOverGameObject()) // something with friendly mask got hit
            {
                try
                {
                    // Click & L.Shift+Click
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        // klikni?cie z Shiftem
                        UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        //klikni?cie normalne
                        UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                    }
                }
                catch (NullReferenceException) { }


            }
            else // no hit on anything or ground
            {
                if (!Input.GetKey(KeyCode.LeftShift)) // and shift is not pressed
                {
                    UnitSelection.Instance.DeselectAll();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }
}