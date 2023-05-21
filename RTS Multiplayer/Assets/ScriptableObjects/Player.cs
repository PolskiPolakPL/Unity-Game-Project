using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public int budget;
    public int income;
    public int incomeTime;
    public int experience;
    public int popCap;
}
