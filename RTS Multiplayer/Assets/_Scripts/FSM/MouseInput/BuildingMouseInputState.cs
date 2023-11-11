using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingMouseInputState : MouseInputBaseState
{
    private Ray _ray;
    public override void EnterState(MouseInputStateManager mouseInput)
    {
        UnitSelection.Instance.DeselectAll();
        mouseInput.CameraScript.enabled = false;
        Debug.Log("Current State: BUILDING");
        mouseInput.hit.transform.gameObject.GetComponent<BuildingScript>().ShowItemList();
    }

    public override void UpdateState(MouseInputStateManager mouseInput)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = mouseInput.camera.ScreenPointToRay(Input.mousePosition);//strzela promieniem
            if(Physics.Raycast(_ray, out mouseInput.hit, Mathf.Infinity))
            {
                string hitTag = mouseInput.hit.collider.tag;
                if(hitTag == "Facility" && !EventSystem.current.IsPointerOverGameObject())
                {
                    mouseInput.hit.transform.gameObject.GetComponent<BuildingScript>().ShowItemList();
                }
                else if (!EventSystem.current.IsPointerOverGameObject())//Trafienie poza budynkiem
                {
                    mouseInput.SwitchState(mouseInput.DefaultState);
                }
            }
        }
    }
}
