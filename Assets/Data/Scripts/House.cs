using outline2;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Outline), typeof(BoxCollider))]
public class House : EventClick
{
    [SerializeField] private PropertiesSo propertiesSo;
    [SerializeField] private CanvasGroup houseCanvasGroup;
    [SerializeField] private TextMeshProUGUI houseNameText;
    
    private LookAtCamera _lookAtCamera;
    private Outline _houseOutline;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        EventManager.Instance.OnBackButtonPressed += EventManager_BackButtonPressed;

        _boxCollider = GetComponent<BoxCollider>();
        _houseOutline = GetComponent<Outline>();
        _lookAtCamera = houseCanvasGroup.GetComponent<LookAtCamera>();

        Initialize();
    }

    private void Initialize()
    {
        _houseOutline.enabled = false;
        houseNameText.text = propertiesSo.houseName;
        HouseSetup(true);
    }

    private void EventManager_BackButtonPressed()
    {
        HouseSetup(true);
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        HouseSetup(false);
        EventManager.Instance.HousePressed();
    }

    private void HouseSetup(bool enable)
    {
        houseCanvasGroup.alpha = enable ? 1f : 0f;
        houseCanvasGroup.interactable = enable;
        houseCanvasGroup.blocksRaycasts = enable;
        houseCanvasGroup.ignoreParentGroups = enable;

        _lookAtCamera.enabled = enable;
        _boxCollider.enabled = enable;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        _houseOutline.enabled = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        _houseOutline.enabled = false;
    }
}
