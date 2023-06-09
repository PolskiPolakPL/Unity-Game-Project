using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    [SerializeField] UnitType type;
    public AudioClip fireSound;
    public int health;
    public int cost;
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public int attackDamage;
    public float accuracy;
}
