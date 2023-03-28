using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    Camera cam;
    public GameObject groundMarker;
    public LayerMask friendlyMask;

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

                //sprawdza, czy promie� trafi� jednostk�
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, friendlyMask))
            {
                // Click & L.Shift+Click
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // klikni�cie z Shiftem
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    //klikni�cie normalne
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }

            }
            else
            {
                //klikni�cie gdziekolwiek indziej
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
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
