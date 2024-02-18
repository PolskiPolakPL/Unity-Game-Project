using UnityEngine;
using UnityEngine.AI;

public class BarracksScript : MonoBehaviour
{
    [SerializeField] PlayerScript playerScript;
    [SerializeField] Transform target;
    public GameObject itemList;
    private void Start()
    {
        if (target == null)
            target = transform.GetChild(0);
    }

    public void RecruitUnit(Unit unitSO)
    {
        playerScript.SpendMoney(unitSO.cost);
        GameObject _unitGO = Instantiate(unitSO.unitGO, transform.position, transform.rotation, playerScript.UnitsT);
        NavMeshAgent _unitAI = _unitGO.GetComponent<NavMeshAgent>();
        _unitAI.SetDestination(target.position);
    }
    public void ShowItemList()
    {
        RectTransform unitListRectTransform = itemList.GetComponent<RectTransform>();
        float unitListWidth = unitListRectTransform.rect.width;
        float unitListHeight = unitListRectTransform.rect.height;
        Vector3 unitListPosition = new Vector3(Input.mousePosition.x + (unitListWidth / 4), Input.mousePosition.y - (unitListHeight / 4), 0f);
        unitListRectTransform.position = unitListPosition;
        itemList.SetActive(true);
    }
    public void HideItemList()
    {
        itemList.SetActive(false);
    }
}