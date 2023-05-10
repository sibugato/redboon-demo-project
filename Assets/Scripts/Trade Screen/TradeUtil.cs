using static SaveSystem;

public static class TradeUtil
{
    // В рамках данной демонстрации у торговца фиксированная наценка
    private static float _merchatPriceMultiplier = 1.25f;

    public static bool TryToBuyItem(Item item)
    {
        if (GetSave().PlayerGoldAmount >= CalculateMerchantItemPrice(item))
        {
            BuyItem(item);
            return true;
        }
        return false;
    }

    public static void BuyItem(Item item)
    {
        GetSave().MerchantItems.Remove(item);
        GetSave().PlayerItems.Add(item);
        GetSave().PlayerGoldAmount -= CalculateMerchantItemPrice(item);
    }

    public static void SellItem(Item item)
    {
        GetSave().PlayerItems.Remove(item);
        GetSave().MerchantItems.Add(item);
        GetSave().PlayerGoldAmount += item.Price;
    }


    public static int CalculateMerchantItemPrice(Item item)
    {
        return (int)(item.Price * _merchatPriceMultiplier);
    }
}
