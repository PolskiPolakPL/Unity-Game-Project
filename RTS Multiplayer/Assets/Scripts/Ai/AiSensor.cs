using System.Collections.Generic;
using UnityEngine;

public class AiSensor : MonoBehaviour
{
    [SerializeField] LayerMask scanLayers;
    [SerializeField] LayerMask blockLayers;
    [SerializeField] float scanFrequency;
    public float viewRange;
    [SerializeField] float eyeHeight;

    public List<GameObject> visibleObjects = new List<GameObject>();

    private float scanInterval;
    private float scanTimer;
    private Collider[] detectedObjects;

    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer = scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        detectedObjects = Physics.OverlapSphere(transform.position, viewRange, scanLayers);

        visibleObjects.Clear();
        for (int i = 0; i < detectedObjects.Length; i++)
        {
            GameObject gameObject = detectedObjects[i].gameObject;
            if (isInSight(gameObject))
            {
                visibleObjects.Add(gameObject);
            }
        }
    }

    private bool isInSight(GameObject gameObject)
    {
        Vector3 offset = new Vector3(0, eyeHeight, 0);
        Vector3 start = transform.position + offset;
        Vector3 end = gameObject.transform.position + offset;

        return !Physics.Linecast(start, end, blockLayers);
    }
}
