using UnityEngine;

public class UIConditions : MonoBehaviour
{
    public Condition Health;
    public Condition Energy;
    private void Start()
    {
        CharacterManager.Instance.Player.PlayerCondition.UICondition = this;
    }
}
