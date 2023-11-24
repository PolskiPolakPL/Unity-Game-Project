using UnityEngine;
using UnityEngine.UI;

public class FloatingBarScript : MonoBehaviour
{
    Slider slider;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    public void UpdateBarValue(float currentValue, float maxValue)
    {
        slider = gameObject.GetComponent<Slider>();
        if(target==null)
            target = transform.parent.parent;
        slider.value = currentValue/maxValue;
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position = target.position + offset;
    }
}
