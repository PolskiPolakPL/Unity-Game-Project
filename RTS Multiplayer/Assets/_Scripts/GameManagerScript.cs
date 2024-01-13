using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //Singleton
    private static GameManagerScript instance;
    public static GameManagerScript Instance { get { return instance; } }
    private void Awake()
    {
        //je¿eli istnieje instancja tej klasy, zniszcz tê instancjê
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //RtsGameManager
    [SerializeField] PlayerScript player1Script;
    void Start()
    {

    }

    public bool isUnitRecruitmentValid(Unit unitSO)
    {
        player1Script.UpdatePopulationCount();
        return unitSO.cost <= player1Script.money && player1Script.population < player1Script.popCap;
    }
}
