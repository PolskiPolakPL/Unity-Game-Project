using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int playerHP;
    public int popCap;
    public int money;
    public int passiveIncome;
    public int ammo;
}
