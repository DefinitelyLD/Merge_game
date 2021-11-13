using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public Sprite icon;
}
public enum ItemType
{
    level_1,
    level_2,
    level_3,
    level_4,
    level_5,
}
