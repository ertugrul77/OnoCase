using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CanvasGroup), typeof(LookAtCamera))]
public class PropertiesCanvasController : MonoBehaviour
{
    [SerializeField] private PropertiesSo propertiesSo;
    [SerializeField] private Button backButton;
    [SerializeField] private Button linkButton;

    [Header("Section 1")] 
    [SerializeField] private TextMeshProUGUI houseText;

    [Header("Section 2")] 
    [SerializeField] private TextMeshProUGUI orderIDText;
    [SerializeField] private TextMeshProUGUI ingredientIDText;

    [Header("Section 3")] 
    [SerializeField] private Image orderFillImage;
    [SerializeField] private TextMeshProUGUI orderAmountText;
    [SerializeField] private Image oeeFillImage;
    [SerializeField] private TextMeshProUGUI oeeAmountText;
    [SerializeField] private float oeeChangeTime = 60f;

    [Header("Section 4")] 
    [SerializeField] private TextMeshProUGUI stoveTemperatureText;
    [SerializeField] private TextMeshProUGUI stoveWeightText;
    [SerializeField] private TextMeshProUGUI topMoldTemperatureText;
    [SerializeField] private TextMeshProUGUI bottomMoldTemperatureText;
    [SerializeField] private TextMeshProUGUI accelerationText;
    [SerializeField] private TextMeshProUGUI pressureText;
    [SerializeField] private float changeTime = 1f;

    #region Local Variables

    private CanvasGroup _canvasGroup;
    private LookAtCamera _lookAtCamera;
    private Animator _animator;
    private string _openLinkAddress;
    private float _randomOfChange;
    private bool _isOpen;

    private int _orderIDNumber;
    private int _ingredientIDNumber;

    private float _orderAmount;
    private float _orderMaxAmount;

    private int _oeeAmount;
    private int _oeeMinAmount;
    private int _oeeMaxAmount;
    private float _oeeTimer = 0f;

    private int _stoveTemperature;
    private int _stoveWeight;
    private int _topMoldTemperature;
    private int _bottomMoldTemperature;
    private float _acceleration;
    private float _pressure;
    private float _section4Timer;
    
    private static readonly int Anim = Animator.StringToHash("Anim");
    private static readonly int Empty = Animator.StringToHash("Empty");
    private bool _animationPlaying;

    #endregion

    private void Awake()
    {
        EventManager.Instance.OnHousePressed += EventManager_OnHousePressed;
        EventManager.Instance.OnBackButtonPressed += EventManager_OnBackButtonPressed;

        linkButton.onClick.AddListener(LinkButtonPressed);

        _canvasGroup = GetComponent<CanvasGroup>();
        _lookAtCamera = GetComponent<LookAtCamera>();
        _animator = GetComponent<Animator>();

        Initialize();
    }
    private void EventManager_OnBackButtonPressed()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(Vector3.zero, 1f).OnComplete(() => { SetCanvasGroup(false); });
    }
    private void EventManager_OnHousePressed()
    {
        SetCanvasGroup(true);

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f);
    }
    private void Initialize()
    {
        SetCanvasGroup(false);
        _openLinkAddress = propertiesSo.linkAddress;
        _randomOfChange = propertiesSo.rangeOfChange;

        _orderIDNumber = propertiesSo.orderIDNumber;
        _ingredientIDNumber = propertiesSo.ingredientIDNumber;

        _oeeMinAmount = propertiesSo.minOee;
        _oeeMaxAmount = propertiesSo.maxOee;
        _oeeAmount = (int)((_oeeMinAmount + _oeeMaxAmount) / 2);
        oeeFillImage.fillAmount = _oeeAmount / _orderMaxAmount;
        oeeAmountText.text = "%" + _oeeAmount;

        _orderAmount = 0f;
        _orderMaxAmount = propertiesSo.orderMaxAmount;
        orderAmountText.text = _orderAmount + "/" + _orderMaxAmount;
        orderFillImage.fillAmount = _orderAmount / _orderMaxAmount;

        orderIDText.text = propertiesSo.orderIDType + "-" + _orderIDNumber;
        ingredientIDText.text = propertiesSo.ingredientIDType + "-" + _ingredientIDNumber;

        _stoveTemperature = propertiesSo.stoveTemperature;
        stoveTemperatureText.text = _stoveTemperature + "°C";

        _stoveWeight = propertiesSo.stoveWeight;
        stoveWeightText.text = _stoveWeight + "kg";

        _topMoldTemperature = propertiesSo.topMoldTemperature;
        topMoldTemperatureText.text = _topMoldTemperature + "°C";

        _bottomMoldTemperature = propertiesSo.bottomMoldTemperature;
        bottomMoldTemperatureText.text = _bottomMoldTemperature + "°C";

        _acceleration = propertiesSo.acceleration;
        accelerationText.text = _acceleration + "mm/s<sup>2</sup>";

        _pressure = propertiesSo.pressure;
        pressureText.text = _pressure + "bar";
    }
    private void Update()
    {
        if (!_isOpen) return;

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_orderAmount >= _orderMaxAmount)
            {
                Debug.Log("You reached Maximum Amount!");
                return;
            }
            ChangeOrderAndIngredient();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (_animationPlaying) return;

            _animationPlaying = true;
            _animator.SetTrigger(Anim);
            StartCoroutine(Danger());
            
            Invoke("BackToNormal", 5f);
        }

        ChangeOee();
        Section4Change();
    }
    private void ChangeOrderAndIngredient()
    {
        // Changing Order and Ingredient ID Numbers
        _orderIDNumber++;
        _ingredientIDNumber++;
        orderIDText.text = propertiesSo.orderIDType + "-" + _orderIDNumber;
        ingredientIDText.text = propertiesSo.ingredientIDType + "-" + _ingredientIDNumber;

        // Changing Order Amount
        _orderAmount += propertiesSo.orderIncreaseAmount;
        _orderAmount = Mathf.Clamp(_orderAmount, 0, _orderMaxAmount);
        orderAmountText.text = _orderAmount + "/" + _orderMaxAmount;
        orderFillImage.fillAmount = _orderAmount / _orderMaxAmount;
    }
    private void ChangeOee()
    {
        _oeeTimer += Time.deltaTime;
        if (!(_oeeTimer >= oeeChangeTime)) return; // Times Up!

        _oeeTimer = 0f;
        _oeeAmount = GetRandomNumber(_oeeMinAmount, _oeeMaxAmount);

        oeeFillImage.fillAmount = _oeeAmount / 100f;
        oeeAmountText.text = "%" + _oeeAmount;
    }
    private void Section4Change()
    {
        _section4Timer += Time.deltaTime;
        if (!(_section4Timer >= changeTime)) return; // Times Up!
        _section4Timer = 0f;

        // Change Stove Temperature
        _stoveTemperature = GetRandomNumberPercentage10(_stoveTemperature);
        stoveTemperatureText.text = _stoveTemperature + "°C";

        // Change Stove Weight
        _stoveWeight = GetRandomNumberPercentage10(_stoveWeight);
        stoveWeightText.text = _stoveWeight + "kg";

        // Change Top Mold Temperature
        _topMoldTemperature = GetRandomNumberPercentage10(_topMoldTemperature);
        topMoldTemperatureText.text = _topMoldTemperature + "°C";

        // Change Bottom Mold Temperature
        _bottomMoldTemperature = GetRandomNumberPercentage10(_bottomMoldTemperature);
        bottomMoldTemperatureText.text = _bottomMoldTemperature + "°C";

        // Change Acceleration
        _acceleration = GetRandomNumberPercentage10(_acceleration);
        accelerationText.text = _acceleration.ToString("F2") + " mm/s<sup>2</sup>";

        // Change Pressure
        _pressure = GetRandomNumberPercentage10(_pressure);
        pressureText.text = _pressure.ToString("F3") + " bar";
    }
    private void LinkButtonPressed()
    {
        Application.OpenURL(_openLinkAddress);
    }
    private void SetCanvasGroup(bool enable)
    {
        _isOpen = enable;
        _canvasGroup.alpha = enable ? 1f : 0f;
        _canvasGroup.interactable = enable;
        _canvasGroup.blocksRaycasts = enable;
        _canvasGroup.ignoreParentGroups = enable;

        backButton.gameObject.SetActive(enable);
        _lookAtCamera.enabled = enable;
    }
    private static int GetRandomNumber(int min, int max)
    {
        var randomNumber = Random.Range(min, max);
        return randomNumber;
    }
    private int GetRandomNumberPercentage10(int value)
    {
        var min = value - (value * _randomOfChange);
        var max = value + (value * _randomOfChange);
        var randomNumber = Random.Range(min, max);
        return (int)randomNumber;
    }
    private float GetRandomNumberPercentage10(float value)
    {
        var min = value - (value * _randomOfChange);
        var max = value + (value * _randomOfChange);
        var randomNumber = Random.Range(min, max);
        return randomNumber;
    }
    private void BackToNormal()
    {
        _animationPlaying = false;
        _isOpen = true;
        _animator.SetTrigger(Empty);
        oeeChangeTime = _startOeeChangeTime;
        changeTime = _startChangeTime;
    }

    private float _startOeeChangeTime;
    private float _startChangeTime;
    private IEnumerator Danger()
    {
        Reset();
        while (_animationPlaying)
        {
            ChangeOee();
            Section4Change();
            yield return new WaitForEndOfFrame();
        }
    }
    private void Reset()
    {
        _isOpen = false;
        _startOeeChangeTime = oeeChangeTime;
        _startChangeTime = changeTime;
        changeTime = 0.5f;
        oeeChangeTime = 0.5f;
    }
}