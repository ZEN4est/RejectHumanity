using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Settings/Item", order = 0)]
public class ItemSettings : ScriptableObject
{
    public Sprite sprite;
    public ItemType type;
}