using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public Unit unit;
    void Start()
    {
        Instantiate(unit.unitPrefab,transform.position,Quaternion.identity,transform);
        UnitSelection.Instance.unitsList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitsList.Remove(this.gameObject);
        if (UnitSelection.Instance.unitsSelected.Contains(this.gameObject))
            UnitSelection.Instance.unitsSelected.Remove(this.gameObject);
    }
}