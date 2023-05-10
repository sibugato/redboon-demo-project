using System.Collections.Generic;
using UnityEngine;

public class SpritesHolder : MonoBehaviour
{
    public static SpritesHolder Sprites;
    [SerializeField] private List<Sprite> _itemSprites;

    /*
        0 - weapon
        1 - armour
        2 - helmet
        2 - shield
        3 - scroll
        4 - food
        5 - potion
        6 - treasure
    */

    private void Awake()
    {
        // Singltone
        if (Sprites == null)
        {
            Sprites = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetItemSprite(Item.Type itemType)
    {
        switch (itemType)
        {
            case Item.Type.weapon: return _itemSprites[0];
            case Item.Type.armour: return _itemSprites[1];
            case Item.Type.helmet: return _itemSprites[2];
            case Item.Type.shield: return _itemSprites[3];
            case Item.Type.scroll: return _itemSprites[4];
            case Item.Type.food: return _itemSprites[5];
            case Item.Type.potion: return _itemSprites[6];
            case Item.Type.treasure: return _itemSprites[7];
            default: return null;
        }
    }
}
