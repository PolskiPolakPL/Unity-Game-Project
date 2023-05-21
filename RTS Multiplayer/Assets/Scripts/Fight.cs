using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    public Unit stats;

    [SerializeField] private bool bulletInChamber = true;

    [SerializeField] private int bullets;

    public GameObject target;

    private Coroutine reloadCoroutine = null;

    private bool reloading = false;
    private bool cycling = false;

    // Start is called before the first frame update
    void Start()
    {
        bullets = stats.magSize;
    }

    // Update is called once per frame
    void Update()
    {
        target = gameObject.GetComponent<AiTargetingSystem>().target;

        if (bullets <= 0 && !reloading)
        {
            reloading = true;
            StartCoroutine(ReloadMag());
        }

        if (bullets > 0 && !bulletInChamber && !cycling)
        {
            cycling = true;
            StartCoroutine(CycleBolt());
        }

        if (target != null && bulletInChamber && !target.GetComponent<UnitScript>().isDead)
        {
            Shot();
        }
    }

    IEnumerator ReloadMag()
    {
        yield return new WaitForSeconds(stats.reloadTime);
        bullets = stats.magSize;
        reloading = false;
    }

    IEnumerator CycleBolt()
    {
        yield return new WaitForSeconds(stats.cycleTime);
        bullets--;
        bulletInChamber = true;
        cycling = false;
    }

    private void Shot()
    {
        float hitTry = Random.Range(0f, 1f);
        float chance = CalculateHitChance();

        if (hitTry < chance)
        {
            target.GetComponent<UnitScript>().TakeDamage(stats.damage);
            target = null;
        }
        bulletInChamber = false;    
    }

    private float CalculateHitChance()
    {
        float distance = Vector3.Magnitude(target.transform.position - transform.position);
        float targetSize = target.GetComponent<Fight>().stats.bodySize;
        float accuracy = 0f;

        if (distance < stats.shortRange)
        {
            accuracy = stats.shortAccuracy;
        }
        if (distance < stats.midRange)
        {
            accuracy = stats.midAccuracy;
        }
        if (distance < stats.longRange)
        {
            accuracy = stats.longAccuracy;
        }

        return accuracy * targetSize;
    }
}
