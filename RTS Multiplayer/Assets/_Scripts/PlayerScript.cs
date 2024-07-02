using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] PlayerStats Stats;
    public Transform UnitsT;
    public int maxHp { get; private set; }
    public int hp;
    public int money;
    public int income;
    public float passiveIncomeRate;
    public int ammo;
    public int popCap;
    public int population;
    // Start is called before the first frame update

    private void Awake()//it is needed to guarantee an update on Enemy's health bar
    {
        maxHp = Stats.playerHP;
        hp = Stats.playerHP;
        money = Stats.money;
        income = Stats.passiveIncome;
        passiveIncomeRate = Stats.passiveIncomeRate;
        ammo = Stats.ammo;
        popCap = Stats.popCap;
        UpdatePopulationCount();
        StartCoroutine(passiveIncomeCoroutine());
    }
    private void Start()
    {
        if (UnitsT == null)
            UnitsT = transform.GetChild(0);
    }
    private void Update()
    {
        UpdatePopulationCount();
    }
    /// <summary>
    /// Checks if Player can afford a Unit.
    /// </summary>
    /// <param name="unitSO">Unit's ScriptableObject</param>
    /// <returns>
    /// <c>True</c> if Unit's cost is less or equal to Player's money AND population is lower than population capacity.
    /// </returns>
    public bool CanAffordUnit(Unit unitSO)
    {
        UpdatePopulationCount();
        return unitSO.cost <= money && population < popCap;
    }



    public void UpdatePopulationCount()
    {
        population = UnitsT.childCount;
    }
    public void LooseHealth(int amount)
    {
        hp -= amount;
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

    IEnumerator passiveIncomeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(passiveIncomeRate);
            GainMoney(income);
        }
    }
}
