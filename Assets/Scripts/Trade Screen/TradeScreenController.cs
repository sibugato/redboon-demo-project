using System.Collections.Generic;
using UnityEngine;
using static SaveSystem;

public class TradeScreenController : MonoBehaviour, IObserver
{
    [SerializeField] private Transform _playerItemsScrollPanel;
    [SerializeField] private Transform _merchantItemsScrollPanel;
    [SerializeField] private RectTransform _playerItemsPanelRectTransform;
    [SerializeField] private RectTransform _merchantItemsPanelRectTransform;
    [SerializeField] private GameObject _tradeScreenItemPrefab;
    [SerializeField] private PlayerGoldWidget _playerGoldWidget;
    [SerializeField] private float _distanceBetweenItemsOnGrid;
    private List<GameObject> _itemsOnTheScreenList = new List<GameObject>();
    private float _landscapeItemPanelWidth = 600;
    private float _portraitItemPanelWidth = 405;
    private int _landscapeLimitOfItemsInRow = 3;
    private int _portraitLimitOfItemsInRow = 2;
    private float _thresholdAspectRatio = 1.333f;
    private int _numberOfItemsInRow;
    private float _screenAspectRatio;

    private void Start()
    {
        LoadPlayerProgress();
        SetupPanels();
        _playerGoldWidget.RefreshGoldAmount();
        AudioManager.Audio.ChangeMusic(AudioManager.MusicType.market);
    }

    private void Update()
    {
        // В демонстрационных целях находится в Update. Перенести в функционал смены разрешения при его появлении.
        float currentScreenAspect = Mathf.Round((float) Screen.width / (float) Screen.height * 1000.0f) * 0.001f;
        if (_screenAspectRatio != currentScreenAspect)
        {
            AplyNewScreenAspectRatio(currentScreenAspect);
        }
    }

    private void AplyNewScreenAspectRatio(float aspectRatio)
    {
        _screenAspectRatio = aspectRatio;
        _playerGoldWidget.AplyNewScreenAspectRatio(aspectRatio);

        if (aspectRatio >= _thresholdAspectRatio)
        {
            _numberOfItemsInRow = _landscapeLimitOfItemsInRow;
            _playerItemsPanelRectTransform.sizeDelta = new Vector2(_landscapeItemPanelWidth, _playerItemsPanelRectTransform.sizeDelta.y);
            _merchantItemsPanelRectTransform.sizeDelta = new Vector2(_landscapeItemPanelWidth, _merchantItemsPanelRectTransform.sizeDelta.y);
        }
        else
        {
            _numberOfItemsInRow = _portraitLimitOfItemsInRow;
            _playerItemsPanelRectTransform.sizeDelta = new Vector2(_portraitItemPanelWidth, _playerItemsPanelRectTransform.sizeDelta.y);
            _merchantItemsPanelRectTransform.sizeDelta = new Vector2(_portraitItemPanelWidth, _merchantItemsPanelRectTransform.sizeDelta.y);
        }
        SetupPanels();
    }

    private void SetupPanels()
    {
        // удаление со сцены игровых объектов, представляющих предметы
        if (_itemsOnTheScreenList.Count > 0)
        {
            foreach (GameObject itemGameObject in _itemsOnTheScreenList)
            {
                Destroy(itemGameObject);
            }
            _itemsOnTheScreenList.Clear();
        }

        SetupScrollPanel(GetSave().PlayerItems, _playerItemsScrollPanel, true);
        SetupScrollPanel(GetSave().MerchantItems, _merchantItemsScrollPanel, false);
    }

    private void SetupScrollPanel(List<Item> itemsList, Transform scrollPanel, bool isOwnedByPlayer)
    {
        if (itemsList != null && itemsList.Count > 0)
        {
            Vector2 startingPosition = new Vector2(_distanceBetweenItemsOnGrid, -1 * _distanceBetweenItemsOnGrid);
            int ItemsInCurrentRow = 0;
            int lines = 1;
            TradeScreenItem tradeScreenItem;
            GameObject tradeItem;
            Vector2 itemSize = Vector2.zero;
            RectTransform scrollPanelRect = scrollPanel as RectTransform;
            RectTransform viewPortRect = scrollPanel.parent as RectTransform;

            // сортировка предметов торговца. Сначала доступные для приобретения
            if (!isOwnedByPlayer)
            {
                List<Item> sortedList = new List<Item>();
                foreach (Item item in itemsList)
                {
                    if (GetSave().PlayerGoldAmount < TradeUtil.CalculateMerchantItemPrice(item))
                    {
                        sortedList.Add(item);
                    }
                    else
                    {
                        sortedList.Insert(0, item);
                    }
                }
                itemsList = sortedList;
            }

            // Заполнение сеток предметов на экране
            foreach (Item item in itemsList)
            {
                tradeItem = Instantiate(_tradeScreenItemPrefab, scrollPanel);
                itemSize = tradeItem.GetComponent<RectTransform>().sizeDelta;
                tradeItem.transform.localPosition = startingPosition;
                startingPosition.x += itemSize.x + _distanceBetweenItemsOnGrid;
                tradeScreenItem = tradeItem.GetComponent<TradeScreenItem>();
                tradeScreenItem.ConfigureItem(item, isOwnedByPlayer, _playerItemsPanelRectTransform, _merchantItemsPanelRectTransform);
                tradeScreenItem.AddObserver(this);
                _itemsOnTheScreenList.Add(tradeItem);
                if (++ItemsInCurrentRow >= _numberOfItemsInRow)
                {
                    lines++;
                    ItemsInCurrentRow = 0;
                    startingPosition.x = _distanceBetweenItemsOnGrid;
                    startingPosition.y -= itemSize.y + _distanceBetweenItemsOnGrid;
                }
            }

            // Калибровка размера панели прокрутки предметов
            float DifferenceBetweenHeights = (itemSize.y + _distanceBetweenItemsOnGrid) * viewPortRect.lossyScale.y * lines - CustomShapeUtils.CreateRectFromRectTransform(viewPortRect).size.y;
            if ( DifferenceBetweenHeights >= 0  )
            {
                scrollPanelRect.offsetMin = new Vector2(scrollPanelRect.offsetMin.x, scrollPanelRect.offsetMax.y - DifferenceBetweenHeights / viewPortRect.lossyScale.y);
            }
            else
            {
                scrollPanelRect.offsetMin = new Vector2(scrollPanelRect.offsetMin.x, 0);
                scrollPanelRect.offsetMax = new Vector2(scrollPanelRect.offsetMax.x, 0);
            }
        }
    }

    // Реализован паттерн "Наблюдатель". Если предметы будут куплены или проданы, они "сообщат" об этом сюда
    public void OnNotify(IObserver.Events currentEvent)
    {
        switch (currentEvent)
        {
            case IObserver.Events.player_sold_item:
                {
                    SetupPanels();
                    _playerGoldWidget.RefreshGoldAmount();
                    AudioManager.Audio.SoundPlayOneShot(AudioManager.SoundType.coin_chink);
                    break;
                }
            case IObserver.Events.player_bought_item:
                {
                    SetupPanels();
                    _playerGoldWidget.RefreshGoldAmount();
                    AudioManager.Audio.SoundPlayOneShot(AudioManager.SoundType.coin_chink);
                    break;
                }
        }
    }
}
