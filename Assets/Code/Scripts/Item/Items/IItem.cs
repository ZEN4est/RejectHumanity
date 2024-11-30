public interface IItem
{
    ItemType ItemType { get; }
    void Use();
    void Hide();
    void Show();
}