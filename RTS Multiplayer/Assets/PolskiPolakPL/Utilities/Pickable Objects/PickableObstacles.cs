using UnityEngine;
using UnityEngine.AI;

public class PickableObstacles : MonoBehaviour
{
    [SerializeField] bool carving;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<NavMeshObstacle>())
                continue;
            child.gameObject.AddComponent<NavMeshObstacle>();
            child.gameObject.GetComponent<NavMeshObstacle>().carving = carving;
        }
    }
}
