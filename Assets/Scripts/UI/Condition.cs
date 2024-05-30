using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float CurValue;
    public float StartValue;
    public float MaxValue;
    public Image Image;

    private void Start()
    {
        CurValue = StartValue;
    }

    private void Update()
    {
        Image.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return CurValue / MaxValue;

    }

    public void Add(float amount)
    {
        CurValue += amount;
        Image.fillAmount = GetPercentage();
    }

    public void Sub(float amount)
    {
        CurValue -= amount;
        Image.fillAmount = GetPercentage();
    }
}
