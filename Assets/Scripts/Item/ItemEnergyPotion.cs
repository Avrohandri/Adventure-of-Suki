using UnityEngine;

[CreateAssetMenu(menuName = "Items/Energy Potion")]
public class ItemEnergyPotion : ItemData
{
    [Header("Config")] 
    [SerializeField] private float energy;

    public override void PickUp()
    {
        LevelManager.Instance.SelectedPlayer
            .GetComponent<PlayerEnergy>().RecoverEnergy(energy);
    }
}