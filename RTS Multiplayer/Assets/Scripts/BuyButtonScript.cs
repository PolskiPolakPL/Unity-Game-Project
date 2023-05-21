using UnityEngine;
using TMPro;

public class BuyButtonScript : MonoBehaviour
{
    [SerializeField] Unit unit;
    TMP_Text nameTMP, priceTMP;
    // Start is called before the first frame update
    void Start()
    {
        nameTMP = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        priceTMP = transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        nameTMP.text = unit.name;
        priceTMP.text = unit.cost.ToString() + "$";
    }
}
