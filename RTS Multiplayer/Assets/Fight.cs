using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public Unit stats;

    private float range;
    private float reloadTime;
    private float accuracy;
    private float damage;

    private float reloadProgres = 0;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadProgres < reloadTime)
        {
            reloadProgres += Time.deltaTime;
            return;
        }

        if (target == null)
        {
            target = gameObject.GetComponent<AiTargetingSystem>().target;
            return;
        }

        if (range > Vector3.Magnitude(target.transform.position - transform.position))
        {
            return;
        }


    }

    private void Shot(GameObject target)
    {
        
    }

    private void CalculateHit(GameObject target)
    {
        
    }
}
