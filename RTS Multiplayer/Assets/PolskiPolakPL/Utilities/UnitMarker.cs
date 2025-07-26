using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMarker : MonoBehaviour
{
    [SerializeField] Transform unitsParent;
    [SerializeField] UnitType unitType;
    [SerializeField] KeyCode triggerKey;
    // Start is called before the first frame update

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            MoveUnits();
        }
    }

    void MoveUnits()
    {
        foreach (Transform unit in unitsParent)
        {
            if (unit.IsDestroyed() || unit.GetComponent<UnitScript>().unitType != unitType)
                continue;
            NavMeshAgent agent = unit.GetComponent<NavMeshAgent>();
            agent.SetDestination(transform.position);
        }
    }

}
