using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public Unit stats;

    private bool bulletInChamber = true;

    private int bullets;

    public GameObject target;

    private Coroutine cycleCoroutine = null;
    private Coroutine reloadCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        bullets = stats.magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = gameObject.GetComponent<AiTargetingSystem>().target;
            return;
        }

        if (bulletInChamber)
        {
            Shot();
        }
    }

    IEnumerator ReloadMag()
    {
        Debug.Log("Reload");
        yield return new WaitForSeconds(stats.reloadTime);
        bullets = stats.magSize;
        reloadCoroutine = null;
    }

    IEnumerator CycleBolt()
    {
        Debug.Log("Cycle");
        while (bullets == 0)
        {
            if (reloadCoroutine == null)
            {
                reloadCoroutine = StartCoroutine(ReloadMag());
            }
        }
        yield return new WaitForSeconds(stats.cycleTime);
        bullets--;
        bulletInChamber = true;
        cycleCoroutine = null;
    }

    private void Shot()
    {
        Debug.Log("SHOT");
        float hitTry = Random.Range(0f, 1f);
        
        if (hitTry < CalculateHitChance())
        {
            target.GetComponent<UnitScript>().TakeDamage(stats.damage);
        }
        bulletInChamber = false;

        if (cycleCoroutine == null)
        {
            cycleCoroutine = StartCoroutine(CycleBolt());
        }     
    }

    private float CalculateHitChance()
    {
        float distance = Vector3.Magnitude(target.transform.position - transform.position);
        float targetSize = target.GetComponent<Fight>().stats.bodySize;
        float accuracy = 0;

        if (distance < stats.shortAccuracy)
        {
            accuracy = stats.shortAccuracy;
        }
        if (distance < stats.midAccuracy)
        {
            accuracy = stats.shortAccuracy;
        }
        if (distance < stats.longAccuracy)
        {
            accuracy = stats.shortAccuracy;
        }

        return accuracy * targetSize;
    }
}
