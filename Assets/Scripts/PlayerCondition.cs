using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UIConditions UICondition;

    Condition _health { get { return UICondition.Health; } }
    Condition _energy { get { return UICondition.Energy; } }

    Coroutine healthCo;
    Coroutine energyCo;
    private void Awake()
    {

    }

    public void Heal(float amount)
    {
        if (healthCo != null)
        {
            StopCoroutine(healthCo);

        }
        healthCo = StartCoroutine(Healing(amount));
    }

    public void Damage(float amount)
    {
        _health.Sub(amount);
    }

    public void RestoreEnergy(float amount)
    {
        _energy.Add(amount);
    }

    IEnumerator Healing(float amount)
    {
        for (float i = 0; i < 1; i+= Time.deltaTime)
        {
            _health.Add(amount / (1 / Time.deltaTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if(_health.CurValue % 1 != 0)
        {
            _health.Add((Mathf.Round(_health.CurValue)) - _health.CurValue);
        }
    }
}