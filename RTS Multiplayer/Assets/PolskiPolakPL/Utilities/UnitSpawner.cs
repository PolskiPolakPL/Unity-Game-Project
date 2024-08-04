using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    public Transform unitsParent;
    [SerializeField] KeyCode triggerKey;
    public UnitType UnitType {  get; private set; }

    private void Start()
    {
        UnitType = unitPrefab.GetComponent<UnitScript>().unitType;
    }

    void SpawnUnit()
    {
        Instantiate(unitPrefab,transform.position,transform.rotation,unitsParent);
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(triggerKey))
        {
            SpawnUnit();
        }
    }
}
