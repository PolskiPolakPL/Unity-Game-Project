using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTargetingSystem : MonoBehaviour
{
    public GameObject target;
    public AiSensor sensor;

    private void Start()
    {
        sensor = GetComponent<AiSensor>();
    }

    private void Update()
    {
        if (TargetsInSight())
        {
            if (target == null)
            {
                TargetClosest();
            }
        }
        else
        {
            target = null;
        }    
    }

    private void TargetClosest()
    {      
        int closestId = 0;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < sensor.visibleObjects.Count; i++)
        {
            float distance = Vector3.Magnitude(transform.position - sensor.visibleObjects[i].transform.position);
            if (distance < minDistance)
            {
                closestId = i;
                minDistance = distance;
            }
        }
        target = sensor.visibleObjects[closestId];
    }

    private bool TargetsInSight()
    {
        return sensor.visibleObjects != null && sensor.visibleObjects.Count != 0;
    }
}
