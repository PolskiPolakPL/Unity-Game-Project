using UnityEngine;

public class MinimapSystem : MonoBehaviour
{
    public static MinimapSystem Instance { get; private set; }
    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
