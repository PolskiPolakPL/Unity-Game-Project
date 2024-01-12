using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingMouseInputState : MouseInputBaseState
{
    private Ray _ray;
    private BuildingScript _script;
    public override void EnterState(MouseInputStateManager mouseInput)
    {
        UnitSelection.Instance.DeselectAll();
        mouseInput.UnitClick.enabled = false;
        mouseInput.UnitDrag.enabled = false;
        mouseInput.CameraScript.enabled = false;
        Debug.Log("Current State: BUILDING");
        _script = mouseInput.Hit.transform.gameObject.GetComponent<BuildingScript>();
        _script.ShowItemList();
    }

    public override void UpdateState(MouseInputStateManager mouseInput)
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = mouseInput.Cam.ScreenPointToRay(Input.mousePosition);//strzela promieniem
            if(Physics.Raycast(_ray, out mouseInput.Hit, Mathf.Infinity))
            {
                string hitTag = mouseInput.Hit.collider.tag;
                if(hitTag == "Facility" && !EventSystem.current.IsPointerOverGameObject())
                {
                    mouseInput.Hit.transform.gameObject.GetComponent<BuildingScript>().ShowItemList();
                }
                else if (!EventSystem.current.IsPointerOverGameObject())//Trafienie poza budynkiem (zmiana stanu na DefaultState)
                {
                    _script.HideItemList();
                    mouseInput.SwitchState(mouseInput.DefaultState);
                }
            }
        }
    }
}
