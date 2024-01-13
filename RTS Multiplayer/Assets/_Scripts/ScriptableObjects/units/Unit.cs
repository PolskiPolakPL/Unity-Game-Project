using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public string unitName;
    public GameObject unitGO;
    public AudioClip fireSound;
    public int health;
    public int cost;
    public float speed;
    public float attackRange;
    public float attackSpeed;
    public int attackDamage;
    public float accuracy;
}
