using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [Header("Weapon")]
    [SerializeField] private Color weaponCommonColor;
    [SerializeField] private Color weaponRareColor;
    [SerializeField] private Color weaponEpicColor;
    [SerializeField] private Color weaponLegendaryColor;

    public PlayerConfig Player { get; set; }
    
    public Color GetWeaponNameColor(WeaponRarity rarity)
    {
        switch (rarity)
        {
            case WeaponRarity.Common: return weaponCommonColor;
            case WeaponRarity.Rare: return weaponRareColor;
            case WeaponRarity.Epic: return weaponEpicColor;
            case WeaponRarity.Legendary: return weaponLegendaryColor;
        }
        
        return Color.white;
    }
}
