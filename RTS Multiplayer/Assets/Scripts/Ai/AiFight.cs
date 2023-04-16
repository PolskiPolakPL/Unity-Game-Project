using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFight : MonoBehaviour
{
    public AiWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<AiWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
