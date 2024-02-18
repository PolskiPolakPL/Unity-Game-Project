
using UnityEngine;

public class DefaultMouseInputState : MouseInputBaseState
{
    //click
    private Ray _ray;
    public override void EnterState(MouseInputStateManager mouseInput)
    {
        mouseInput.CameraScript.enabled = true;
        mouseInput.UnitClick.enabled = true;
        mouseInput.UnitDrag.enabled = true;
        Debug.Log("Current State: DEFAULT");
    }

    public override void UpdateState(MouseInputStateManager mouseInput)
    {
        //LEWY PRZYCISK MYSZY
        if (Input.GetMouseButtonDown(0))
        {
            _ray = mouseInput.Cam.ScreenPointToRay(Input.mousePosition);//strzela promieniem
            if(Physics.Raycast(_ray, out mouseInput.Hit,Mathf.Infinity))
            {
                string hitTag = mouseInput.Hit.collider.tag;
                if (hitTag.Equals("Facility"))//trafia budynek (zmiana stanu na BuildingState)
                {
                    mouseInput.SwitchState(mouseInput.BuildingState);
                }
            }
        }
    }
}
