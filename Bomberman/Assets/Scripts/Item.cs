using UnityEngine;

public class Item : MonoBehaviour 
{
    enum ItemType
    {
        BlastRadius,
        ExtraBomb,
        SpeedIncrease
    }

    [SerializeField] private ItemType currentItemType = ItemType.BlastRadius;

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player")) return;
        PlayerController player = other.GetComponent<PlayerController>();

        switch(currentItemType)
        {
            case ItemType.BlastRadius:
                player.IncreaseBombPower();
                break;
            case ItemType.ExtraBomb:
                player.IncreaseBombMax();
                break;
            case ItemType.SpeedIncrease:
                player.IncreaseMoveSpeed();
                break;
        }
        Destroy(gameObject);
    }
}