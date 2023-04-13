using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitsList = new List<GameObject> ();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelection instance;
    public static UnitSelection Instance { get { return instance; } }

    private void Awake()
    {
        //je¿eli istnieje instancja tej klasy, zniszcz tê instancjê
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        unitToAdd.GetComponent<UnitController>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitController>().enabled = true;
        }
        else
        {
            unitToAdd.GetComponent<UnitController>().enabled = false;
            unitToAdd.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitController>().enabled = true;
        }
    }

    public void DeselectAll()
    {
        foreach(GameObject unit in unitsSelected)
        {
            unit.GetComponent<UnitController>().enabled = false;
            unit.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        unitsSelected.Clear();
    }

}
