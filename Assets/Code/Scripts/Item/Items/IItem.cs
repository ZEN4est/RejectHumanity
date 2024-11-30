public interface IItem
{
    ItemType ItemType { get; }
    void Use();
    void Disable();
    void Enable();
}