using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIScript : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public int maxHp { get; private set; }
    public int hp;
    public int money;
    public int ammo;
    public int popCap;

    private void Awake()
    {
        maxHp = playerStats.playerHP;
        hp = playerStats.playerHP;
        money = playerStats.money;
        ammo = playerStats.ammo;
        popCap = playerStats.popCap;
    }
    public void LooseHealth(int amount)
    {
        hp -= amount;
        PlayerUiManagerScript.Instance.UpdateEnemyPlayerHP();
    }
    public void GainMoney(int amount)
    {
        money += amount;
    }
    public void SpendMoney(int amount)
    {
        money -= amount;
    }
    public void GainAmmo(int amount)
    {
        ammo += amount;
    }
    public void SpendAmmo(int amount)
    {
        ammo -= amount;
    }
    public void ChangePopulationCap(int newPopCap)
    {
        popCap = newPopCap;
    }
}