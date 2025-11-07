using DemiurgEngine.StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StatView : UIBehaviour
{
    public bool displayCurrentValue;
    public int numbersAfterDotInFloat;
    [SerializeField] Image _barImage;
    //TODO implement this
    [SerializeField] Image _fadeBarImage;

    [SerializeField] TMP_Text _text;

    [SerializeField] Stat _statName;
    [SerializeField] StatsController _statsController;
    [SerializeField] int _partsQuantity;
    Stat _stat;
    float _currentBarWidth;

    public StatsController StatsController { get => _statsController; set { _statsController = value; _stat = _statsController.GetStat(_statName); } }

    bool _inited = false;
    public enum StatType
    {
        simple = 0,
        complex
    }
    protected override void Start()
    {
        if (_text != null)
        {
            _stat.onChange += UpdateText;
        }

        if (_barImage != null)
        {
            _stat.onChange += UpdateBar;
        }
    }
    void Update()
    {
        if (_text != null)
        {
            UpdateText(_stat.CurrentValue, _stat.BaseValue);
        }
        if (_barImage != null)
        {
            UpdateBar(_stat.CurrentValue, _stat.BaseValue);
        }
    }
    public void Init()
    {
        if (_statsController == null)
        {
            return;
        }
        _stat = _statsController.GetStat(_statName.name);

        if (_text != null)
        {
            UpdateText(_stat.CurrentValue, _stat.BaseValue);
            _stat.onChange += UpdateText;
        }
        if (_barImage != null)
        {
            UpdateBar(_stat.CurrentValue, _stat.BaseValue);
            _stat.onChange += UpdateBar;
        }
        _inited = true;
    }
    void UpdateText(float current, float max)
    {
        if (displayCurrentValue)
        {
            if (_text)
            {
                _text.text = current.ToString($"F{numbersAfterDotInFloat}") + "/" + max.ToString($"F{numbersAfterDotInFloat}");
            }

        }
        else
        {
            _text.text = max.ToString($"F{numbersAfterDotInFloat}");
        }
    }
    void UpdateBar(float current, float max)
    {
        //_currentBarWidth = Mathf.Round(_health.CurrentValue / _health.Value * _partsQuantity);
        //_barImage.fillAmount = _currentBarWidth / _partsQuantity;
        _barImage.fillAmount = current / max;
    }
}
