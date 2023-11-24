using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/Player")]
public class Player : ScriptableObject
{
    public int budget;
    public int income;
    public float incomeTime;
    public int experience;
    public int popCap;
}
