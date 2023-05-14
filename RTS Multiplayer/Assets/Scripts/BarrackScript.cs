using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarrackScript : MonoBehaviour
{
    public GameObject itemList;
    Camera cam;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))//prawy przycisk myszy
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject == this.gameObject)//trafienie w budynek
                {
                    Debug.Log("Budynek!");
                    ShowitemList();
                    InputManager.Instance.currentState = Selection.BUILDING;
                }
                else if(InputManager.Instance.currentState == Selection.BUILDING)//bycie w stanie BUILDING
                {
                    Debug.Log("Hide!");
                    InputManager.Instance.currentState = Selection.NONE;
                    HideItemList();
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))//lewy przycisk myszy
        {
            
            if(InputManager.Instance.currentState == Selection.BUILDING
                && !EventSystem.current.IsPointerOverGameObject())//w stanie BUILDING i trafienie poza liste
            {
                Debug.Log("Hide!");
                InputManager.Instance.currentState = Selection.NONE;
                HideItemList();
            }
        }
    }
    void ShowitemList()
    {
        // Get the RectTransform component of the unit list UI
        RectTransform unitListRectTransform = itemList.GetComponent<RectTransform>();

        // Calculate the width and height of the unit list UI
        float unitListWidth = unitListRectTransform.rect.width;
        float unitListHeight = unitListRectTransform.rect.height;

        // Set the position of the unit list UI to the upper-left corner of the mouse click position
        Vector3 unitListPosition = new Vector3(Input.mousePosition.x + (unitListWidth / 2f), Input.mousePosition.y - (unitListHeight / 2f), 0f);
        
        // Set the position of the unit list UI to the mouse click position
        unitListRectTransform.position = unitListPosition;
        
        // Enable the unit list UI
        itemList.SetActive(true);
    }
    void HideItemList()
    {
        itemList.SetActive(false);
    }
}