public struct Item
{
    public enum Type
    {
        weapon,
        armour,
        helmet,
        shield,
        scroll,
        food,
        potion,
        treasure
    }

    public int Price { get; }
    public string Name { get; }
    public Type ItemType { get; }

    public Item(Type type, string name, int price)
    {
        Name = name;
        Price = price;
        ItemType = type;
    }
}
