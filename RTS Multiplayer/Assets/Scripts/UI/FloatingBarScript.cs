using UnityEngine;
using UnityEngine.UI;

public class FloatingBarScript : MonoBehaviour
{
    Slider slider;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        target = transform.parent.parent;
    }

    public void UpdateBarValue(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }
}
