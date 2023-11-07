using UnityEngine;

public class DropExperienceScript : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] int xpDeopped;

    private void OnDestroy()
    {
        player.budget += xpDeopped;
    }
}
