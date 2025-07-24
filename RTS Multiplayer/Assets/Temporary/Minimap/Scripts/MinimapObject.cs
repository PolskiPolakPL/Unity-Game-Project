using UnityEngine;

public class MinimapObject : MonoBehaviour
{
    [SerializeField] GameObject minimapPrefab;
    private void Start()
    {
        minimapPrefab.SetActive(true);
    }
}
