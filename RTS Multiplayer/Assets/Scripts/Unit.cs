using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] UnitType type;
    public float health;
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;
    public float accuracity;
}
