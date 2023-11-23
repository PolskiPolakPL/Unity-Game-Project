using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public int maxHp { get; private set; }
    public int hp;
    public int money;
    public int income;
    public int ammo;
    public int popCap;
    public int population;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = playerStats.playerHP;
        hp = playerStats.playerHP;
        money = playerStats.money;
        income = playerStats.passiveIncome;
        ammo = playerStats.ammo;
        popCap = playerStats.popCap;
        population = transform.GetChild(0).childCount;
    }
    private void Update()
    {
        population = transform.GetChild(0).childCount;
    }
    public void LooseHealth(int amount)
    {
        hp -= amount;
        PlayerUiManagerScript.Instance.UpdatePlayerHP();
    }
    public void GainMoney(int amount)
    {
        money += amount;
        PlayerUiManagerScript.Instance.UpdateMoney();
    }
    public void SpendMoney(int amount)
    {
        money -= amount;
        PlayerUiManagerScript.Instance.UpdateMoney();
    }
    public void GainAmmo(int amount)
    {
        ammo += amount;
        PlayerUiManagerScript.Instance.UpdateAmmo();
    }
    public void SpendAmmo(int amount)
    {
        ammo -= amount;
        PlayerUiManagerScript.Instance.UpdateAmmo();
    }
    public void ChangePopulationCap(int newPopCap)
    {
        popCap = newPopCap;
        PlayerUiManagerScript.Instance.UpdatePopulation();
    }
}
