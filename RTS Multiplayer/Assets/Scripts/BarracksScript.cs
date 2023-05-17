using UnityEngine;

public class BarracksScript : MonoBehaviour
{
    public Transform  target, units;
    public GameObject conscriptPrefab, sniperPrefab, machinegunPrefab;
    UnitController controller;
    public void recruitConscript()
    {
        GameObject unit = Instantiate(conscriptPrefab,transform.position,Quaternion.identity, units);

    }
}