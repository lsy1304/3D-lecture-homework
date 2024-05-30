using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public Data data;
    private PlayerCondition condition;

    private void Start()
    {
        condition = CharacterManager.Instance.Player.PlayerCondition;
    }
    public string GetInteractPrompt()
    {
        return $"{data.itemName}\n{data.itemDescription}";
    }

    public void OnInteract()
    {
        if(data.type == ItemType.Consumable)
        {
            for(int i = 0; i<data.consumables.Length; i++)
            {
                switch(data.consumables[i].type)
                {
                    case Consumable.Health:
                        condition.Heal(data.consumables[i].value);
                        break;
                    case Consumable.Energy:
                        condition.RestoreEnergy(data.consumables[i].value);
                        break;
                }
            }
        }
    }
}
