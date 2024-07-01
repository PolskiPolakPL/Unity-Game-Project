using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemListScript : MonoBehaviour
{
    [SerializeField] List<Unit> unitsSOList;
    [SerializeField] GameObject buttonTemplate;
    [SerializeField] BarracksScript barrackScript;
    private GameObject buttonGO;
    private Button button;
    private Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < unitsSOList.Count; i++)
        {
            unit = unitsSOList[i];
            buttonGO = Instantiate(buttonTemplate, transform);
            buttonGO.transform.GetChild(0).GetComponent<TMP_Text>().text = unit.unitName;
            buttonGO.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{unit.cost}$";
            button = buttonGO.GetComponent<Button>();
            button.AddEventListener(unit, BuyUnit);
        }
        Destroy(buttonTemplate);
    }

    void BuyUnit(Unit unit)
    {
        barrackScript.RecruitUnit(unit);
    }

}

public static class ButtonExtention
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}