using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitsList = new List<GameObject> ();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelection instance;
    public static UnitSelection Instance { get { return instance; } }
    [Tooltip("position of selection circle in unit's hierarchy")][SerializeField] int childIndex = 1;

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
        unitToAdd.transform.GetChild(childIndex).gameObject.SetActive(true);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(childIndex).gameObject.SetActive(true);
        }
        else
        {
            unitToAdd.transform.GetChild(childIndex).gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(childIndex).gameObject.SetActive(true);
        }
    }

    public void DeselectAll()
    {
        foreach(GameObject unit in unitsSelected)
        {
            unit.transform.GetChild(childIndex).gameObject.SetActive(false);
        }
        unitsSelected.Clear();
    }

}
