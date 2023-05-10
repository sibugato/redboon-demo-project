using System.Collections.Generic;

public static class SaveSystem
{
    private static SavedData _save;

    // небольшая имитация загрузки
    public static void LoadPlayerProgress()
    {
        _save = new SavedData();
        LoadPlayerStartingItems();
        LoadMerchantStartingItems();
        _save.PlayerGoldAmount = 27;
    }

    public static SavedData GetSave()
    {
        return _save;
    }

    private static void LoadPlayerStartingItems()
    {
        _save.PlayerItems = new List<Item>
        {
            new Item(Item.Type.weapon, "Простой меч", 50),
            new Item(Item.Type.helmet, "Железный шлем", 50),
            new Item(Item.Type.armour, "Железная\nкираса", 100),
            new Item(Item.Type.food, "Сыр", 5),
            new Item(Item.Type.treasure, "Алмаз", 200),
            new Item(Item.Type.treasure, "Алмаз", 200)
        };
    }

    private static void LoadMerchantStartingItems()
    {
        _save.MerchantItems = new List<Item>
        {
            new Item(Item.Type.potion, "Зелье лечения", 50),
            new Item(Item.Type.potion, "Зелье лечения", 50),
            new Item(Item.Type.potion, "Зелье лечения", 50),
            new Item(Item.Type.scroll, "Свиток\n\"Призыв вепря\"", 100),
            new Item(Item.Type.scroll, "Свиток\n\"Очарование\"", 50),
            new Item(Item.Type.scroll, "Свиток\n\"Призыв сыра\"", 130),
            new Item(Item.Type.food, "Сыр", 5),
            new Item(Item.Type.food, "Сыр", 5),
            new Item(Item.Type.food, "Легендарный\nсыр", 800),
        };
    }
}
