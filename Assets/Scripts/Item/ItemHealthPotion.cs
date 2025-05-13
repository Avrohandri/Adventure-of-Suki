using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Potion")]
public class ItemHealthPotion : ItemData
{
    [Header("Config")]
    [SerializeField] private float health;
    
    public override void PickUp()
    {
        LevelManager.Instance.SelectedPlayer
            .GetComponent<PlayerHealth>().RecoverHealth(health);
    }
}