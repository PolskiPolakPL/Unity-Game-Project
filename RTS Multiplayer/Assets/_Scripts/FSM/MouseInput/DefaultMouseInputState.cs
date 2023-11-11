
using UnityEngine;

public class DefaultMouseInputState : MouseInputBaseState
{
    //click
    private Ray _ray;
    //drag-click
    private Rect _selectionBox;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    public override void EnterState(MouseInputStateManager mouseInput)
    {
        DrawGFX(mouseInput.boxGFX);
        mouseInput.itemList.SetActive(false);
        mouseInput.CameraScript.enabled = true;
        Debug.Log("Current State: DEFAULT");
    }

    public override void UpdateState(MouseInputStateManager mouseInput)
    {
        //LEWY PRZYCISK MYSZY
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _selectionBox = new Rect();
            _ray = mouseInput.camera.ScreenPointToRay(Input.mousePosition);//strzela promieniem
            if(Physics.Raycast(_ray, out mouseInput.hit,Mathf.Infinity))
            {
                string hitTag = mouseInput.hit.collider.tag;
                if (hitTag.Equals("Facility"))//trafia budynek
                {
                    mouseInput.SwitchState(mouseInput.BuildingState);
                }
                else if (hitTag.Equals("Infantry"))//Trafienie jednoski
                {
                    ClickUnit(mouseInput);
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift)) //klik bez Shift-a
                    {
                        UnitSelection.Instance.DeselectAll();
                    }
                }
            }
        }
        if (Input.GetMouseButton(0))
        {//trzymanie myszy
            _endPosition = Input.mousePosition;
            DrawGFX(mouseInput.boxGFX);
            DrawSelection();
        }
        if (Input.GetMouseButtonUp(0))
        {//upuszczenie myszy
            SelectUnits(mouseInput.camera);
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            DrawGFX(mouseInput.boxGFX);
        }
        //PRAWY PRZYCISK MYSZY
        if (Input.GetMouseButtonDown(1))
        {
            _ray = mouseInput.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out mouseInput.hit, Mathf.Infinity, mouseInput.groundLayerMask))
            {
                mouseInput.groundMarker.transform.position = mouseInput.hit.point;
                mouseInput.groundMarker.SetActive(false);
                mouseInput.groundMarker.SetActive(true);
            }
        }
    }

    void ClickUnit(MouseInputStateManager mouseInput)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // klikniêcie z Shiftem
            UnitSelection.Instance.ShiftClickSelect(mouseInput.hit.collider.gameObject);
        }
        else
        {
            //klikniêcie normalne
            UnitSelection.Instance.ClickSelect(mouseInput.hit.collider.gameObject);
        }
    }

    //zmienne dla metody 'DrawGFX'
    Vector2 boxStart, boxEnd, boxCenter, boxSize;
    void DrawGFX(RectTransform boxGFX)
    {
        boxStart = _startPosition;
        boxEnd = _endPosition;

        boxCenter = (boxStart + boxEnd) / 2;
        boxGFX.position = boxCenter;

        boxSize = new Vector2(Mathf.Abs(boxEnd.x - boxStart.x), Mathf.Abs(boxEnd.y - boxStart.y));
        boxGFX.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //kalkulacje dla X
        if (Input.mousePosition.x < _startPosition.x)
        {//idzie w lewo
            _selectionBox.xMin = Input.mousePosition.x;
            _selectionBox.xMax = _startPosition.x;
        }
        else
        {//idzie w prawo
            _selectionBox.xMin = _startPosition.x;
            _selectionBox.xMax = Input.mousePosition.x;
        }

        //kalkulacje dla Y
        if (Input.mousePosition.y < _startPosition.y)
        {//zaznacza w dó³
            _selectionBox.yMin = Input.mousePosition.y;
            _selectionBox.yMax = _startPosition.y;
        }
        else
        {//zaznacza w górê
            _selectionBox.yMin = _startPosition.y;
            _selectionBox.yMax = Input.mousePosition.y;
        }

    }

    void SelectUnits(Camera cam)
    {
        //szuka wœród wszystkich jednostek
        foreach (var unit in UnitSelection.Instance.unitsList)
        {
            //czy jednostka jest wewn¹trz box-a
            if (_selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}
