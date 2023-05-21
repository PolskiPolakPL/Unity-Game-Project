using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] UnitType type;
    public int health;
    public int cost;
    public float speed;

    public float shortRange;
    public float midRange;
    public float longRange;

    public float shortAccuracy;
    public float midAccuracy;
    public float longAccuracy;

    public int rateOfFire;
    public int realoadTime;
}
