using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ValueSliderManager : MonoBehaviour
{
    //Remove [HideInInspector] for debugging purposes
    [HideInInspector][SerializeField] TMP_InputField tmpInputField;
    [HideInInspector][SerializeField] Slider slider;

    private void Start()
    {
        slider.onValueChanged?.Invoke(slider.value);
    }

    public void SetIntValue(float value)
    {
        value = Mathf.RoundToInt(value);
        tmpInputField.text = value.ToString();
    }
    public void SetDecimalValue(float value)
    {
        tmpInputField.text = value.ToString("F1");
    }
    public void SetPercentValue(float value)
    {
        float percent = Mathf.RoundToInt((value/slider.maxValue) * 100);
        tmpInputField.text = percent.ToString() + "%";
    }
    public void SetIntValToBar(string valText)
    {
        float value = TextToValue(valText);
        value = Mathf.RoundToInt(value);
        value = MinMaxConstrain(value);
        slider.value = value;
        SetIntValue(value);
    }
    public void SetDecimalValToBar(string valText)
    {
        float value = TextToValue(valText);
        value = MinMaxConstrain(value);
        slider.value = value;
        SetDecimalValue(value);
    }
    public void SetPercentValToBar(string valText)
    {
        float value = TextToValue(valText);
        value = (value / 100)*slider.maxValue;
        value = MinMaxConstrain(value);
        slider.value = value;
        SetPercentValue(value);
    }
    float MinMaxConstrain(float value)
    {
        if(value < slider.minValue)
            value = slider.minValue;
        if(value > slider.maxValue)
            value = slider.maxValue;
        return value;
    }

    float TextToValue(string text)
    {
        float value = slider.value;
        try
        {
            value = float.Parse(text);
        }catch(FormatException)
        {
            slider.onValueChanged?.Invoke(slider.value);
        }
        return value;
    }
    
    void ValidatePercentText(float value)
    {

    }
}