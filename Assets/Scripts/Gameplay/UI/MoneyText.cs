using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    void Awake()
    {
        EventBus.Subscribe<CurrencyEventArgs>(OnMoneyChanged);
    }
    void OnMoneyChanged(CurrencyEventArgs e)
    {
        _text.text = e.current.ToString();
    }
    void OnDestroy()
    {
        EventBus.Unsubscribe<CurrencyEventArgs>(OnMoneyChanged);
    }
}
