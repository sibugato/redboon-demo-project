using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SaveSystem;

public class TradeScreenItem : ObservationObject, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private RectTransform _rectTransform;

    private Item _item;
    private Vector2 _startingPosition;
    private bool _isOwnedByPlayer;
    private RectTransform _playerItemsPanelRectTransform;
    private RectTransform _merchantItemsPanelRectTransform;
    private Transform _startingParent;

    private Vector2 _startingTouchPosition;

    // настройка отображение предмета для сцены
    public void ConfigureItem(Item item, bool isOwnedByPlayer, RectTransform playerItemsPanelRectTransform, RectTransform merchantItemsPanelRectTransform)
    {
        _playerItemsPanelRectTransform = playerItemsPanelRectTransform;
        _merchantItemsPanelRectTransform = merchantItemsPanelRectTransform;

        _item = item;
        _name.text = item.Name;
        _icon.sprite = SpritesHolder.Sprites.GetItemSprite(item.ItemType);
        _isOwnedByPlayer = isOwnedByPlayer;
        _startingPosition = transform.localPosition;
        _startingParent = transform.parent;
        _priceText.text = isOwnedByPlayer ? item.Price.ToString() : TradeUtil.CalculateMerchantItemPrice(item).ToString();
        if (!isOwnedByPlayer && GetSave().PlayerGoldAmount < TradeUtil.CalculateMerchantItemPrice(item))
        {
            _priceText.color = Color.red;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position - _startingTouchPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // родитель меняется для "выхода" из вьюпорта скролл панели и отрисовки перетаскиваемого предмета поверх всего остального 
        _startingTouchPosition = eventData.position - (Vector2) transform.position;
        transform.SetParent(transform.root);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isOwnedByPlayer && CustomShapeUtils.RectOverlapsCheck(_rectTransform, _merchantItemsPanelRectTransform))
        {
            TradeUtil.SellItem(_item);
            NotifyObservers(IObserver.Events.player_sold_item);
        }
        else if (!_isOwnedByPlayer && CustomShapeUtils.RectOverlapsCheck(_rectTransform, _playerItemsPanelRectTransform))
        {
            if (TradeUtil.TryToBuyItem(_item))
            {
                NotifyObservers(IObserver.Events.player_bought_item);
            }
            else
            {
                ResetPosition();
            }
        }
        else
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        transform.SetParent(_startingParent);
        transform.localPosition = _startingPosition;
    }
}
