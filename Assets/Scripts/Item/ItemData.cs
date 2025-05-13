using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public Sprite Icon;

    public virtual void PickUp()
    {
        
    }
}
