using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDrag : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] RectTransform boxGFX;

    Rect selectionBox;
    Vector2 startPosition;
    Vector2 endPosition;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawGFX();
        selectionBox = new Rect();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {//klikni?cie myszy
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {//trzymanie myszy
            endPosition = Input.mousePosition;
            DrawGFX();
            DrawSelection();
        }
        if (Input.GetMouseButtonUp(0))
        {//upuszczenie myszy
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawGFX();
        }
    }

    //zmienne dla metody 'DrawGFX'
    Vector2 boxStart, boxEnd, boxCenter, boxSize;
    void DrawGFX()
    {
        boxStart = startPosition;
        boxEnd = endPosition;

        boxCenter = (boxStart + boxEnd) / 2;
        boxGFX.position = boxCenter;

        boxSize = new Vector2(Mathf.Abs(boxEnd.x - boxStart.x), Mathf.Abs(boxEnd.y - boxStart.y));
        boxGFX.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //kalkulacje dla X
        if (Input.mousePosition.x < startPosition.x)
        {//idzie w lewo
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {//idzie w prawo
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //kalkulacje dla Y
        if (Input.mousePosition.y < startPosition.y)
        {//zaznacza w dó?
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {//zaznacza w gór?
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }

    }

    void SelectUnits()
    {
        //szuka w?ród wszystkich jednostek
        foreach (var unit in UnitSelection.Instance.unitsList)
        {
            //czy jednostka jest wewn?trz box-a
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}