using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    /*
    [Header("Unit Statistics")]
    [SerializeField] float hp = 100;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float viewDistance = 10;
    [SerializeField] float engagmentDistance = 15;
    [SerializeField] float infantryDamage = 0;
    [SerializeField] float vechicleDamage = 0;
    [SerializeField] float buildingDamage = 0;
    */
    // Start is called before the first frame update
    void Start()
    {
        UnitSelection.Instance.unitsList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitsList.Remove(this.gameObject);
    }
}
