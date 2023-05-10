using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SaveSystem;

public class PlayerGoldWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldAmountText;
    [SerializeField] private Image _goldImage;
    [SerializeField] private RectTransform _underlayRectTransform;
    private float _landscapeOffsetX = 650;
    private float _portraitOffsetX = 460;
    private float _thresholdAspectRatio = 1.333f;

    private Tweener _coinSizeTweener;
    private float _coinSizeTweenScaleMultiplier = 1.25f;
    private float _coinSizeTweenDuration = 0.1f;

    public void RefreshGoldAmount()
    {
        _goldAmountText.text = GetSave().PlayerGoldAmount.ToString();
        if (_coinSizeTweener != null)
        {
            _coinSizeTweener.Kill(true);
        }
        _coinSizeTweener = _goldImage.transform.DOScale(Vector3.one * _coinSizeTweenScaleMultiplier, _coinSizeTweenDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void AplyNewScreenAspectRatio(float aspectRatio)
    {
        if (aspectRatio >= _thresholdAspectRatio)
        {
            _underlayRectTransform.offsetMin = new Vector2(_landscapeOffsetX, _underlayRectTransform.offsetMin.y);
            _underlayRectTransform.offsetMax = new Vector2(-_landscapeOffsetX, _underlayRectTransform.offsetMax.y);
        }
        else
        {
            _underlayRectTransform.offsetMin = new Vector2(_portraitOffsetX, _underlayRectTransform.offsetMin.y);
            _underlayRectTransform.offsetMax = new Vector2(-_portraitOffsetX, _underlayRectTransform.offsetMax.y);
        }
    }
}
