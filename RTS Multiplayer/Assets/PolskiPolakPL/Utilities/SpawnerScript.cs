using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] Transform unitsParent;
    [SerializeField] KeyCode triggerKey;

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(triggerKey))
        {
            Instantiate(unitPrefab, transform.position, transform.rotation, unitsParent);
        }
    }
}
