using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    
    [SerializeField] float accuracyNear;
    [SerializeField] float accuracyMid;
    [SerializeField] float accuracyFar;

    [SerializeField] float rangeNear;
    [SerializeField] float rangeMid;
    [SerializeField] float rangeFar;

    [SerializeField] float moveAccuracyMulti;

    [SerializeField] float coverAccuracyMulti;

    [SerializeField] float realoadTime;

    [SerializeField] float maxAmmo;
    [SerializeField] float ammo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
