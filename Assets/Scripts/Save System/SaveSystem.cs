using System.Collections.Generic;

public static class SaveSystem
{
    private static SavedData _save;

    // ��������� �������� ��������
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
            new Item(Item.Type.weapon, "������� ���", 50),
            new Item(Item.Type.helmet, "�������� ����", 50),
            new Item(Item.Type.armour, "��������\n������", 100),
            new Item(Item.Type.food, "���", 5),
            new Item(Item.Type.treasure, "�����", 200),
            new Item(Item.Type.treasure, "�����", 200)
        };
    }

    private static void LoadMerchantStartingItems()
    {
        _save.MerchantItems = new List<Item>
        {
            new Item(Item.Type.potion, "����� �������", 50),
            new Item(Item.Type.potion, "����� �������", 50),
            new Item(Item.Type.potion, "����� �������", 50),
            new Item(Item.Type.scroll, "������\n\"������ �����\"", 100),
            new Item(Item.Type.scroll, "������\n\"����������\"", 50),
            new Item(Item.Type.scroll, "������\n\"������ ����\"", 130),
            new Item(Item.Type.food, "���", 5),
            new Item(Item.Type.food, "���", 5),
            new Item(Item.Type.food, "�����������\n���", 800),
        };
    }
}
