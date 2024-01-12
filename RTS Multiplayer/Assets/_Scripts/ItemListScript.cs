using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemListScript : MonoBehaviour
{
    public GameObject itemList;

    public void ShowItemList()
    {
        // Get the RectTransform component of the unit list UI
        RectTransform unitListRectTransform = itemList.GetComponent<RectTransform>();

        // Calculate the width and height of the unit list UI
        float unitListWidth = unitListRectTransform.rect.width;
        float unitListHeight = unitListRectTransform.rect.height;

        // Set the position of the unit list UI to the upper-left corner of the mouse click position
        Vector3 unitListPosition = new Vector3(Input.mousePosition.x + (unitListWidth/4), Input.mousePosition.y - (unitListHeight/4), 0f);
        
        // Set the position of the unit list UI to the mouse click position
        unitListRectTransform.position = unitListPosition;
        
        // Enable the unit list UI
        itemList.SetActive(true);
    }
    public void HideItemList()
    {
        itemList.SetActive(false);
    }
}