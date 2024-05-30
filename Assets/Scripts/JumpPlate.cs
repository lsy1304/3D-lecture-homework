using UnityEngine;

public class JumpPlate : MonoBehaviour
{
    public float JumpPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IJumpable jumpable))
        {
            jumpable.JumpPlate(JumpPower);
        }
    }
}
