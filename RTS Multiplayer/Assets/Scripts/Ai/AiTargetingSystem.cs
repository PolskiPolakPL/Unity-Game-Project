using System;
using UnityEngine;

[RequireComponent(typeof(AiSensor))]
public class AiTargetingSystem : MonoBehaviour
{
    public GameObject target;
    private AiSensor sensor;
    private void Start()
    {
        sensor = GetComponent<AiSensor>();
    }

    private void Update()
    {
        if (TargetsInSight())
        {
            if (!TargetIsVisible())
            {
                TargetClosest();
            }
        }
        else
        {
            target = null;
        }
        if(target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    private void TargetClosest()
    {      
        int closestId = 0;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < sensor.visibleObjects.Count; i++)
        {
            try
            {
                if (sensor.visibleObjects[i] == null || sensor.visibleObjects[i].GetComponent<UnitScript>().isDead)
                {
                    continue;
                }
                float distance = Vector3.Magnitude(transform.position - sensor.visibleObjects[i].transform.position);
                if (distance < minDistance)
                {
                    closestId = i;
                    minDistance = distance;
                }
            } catch(NullReferenceException) { }
            
        }
        target = sensor.visibleObjects[closestId];
    }

    private bool TargetsInSight()
    {
        return sensor.visibleObjects != null && sensor.visibleObjects.Count > 0;
    }

    private bool TargetIsVisible()
    {
        if (target == null)
        {
            return false;
        }

        return sensor.visibleObjects.Contains(target);
    }
}
