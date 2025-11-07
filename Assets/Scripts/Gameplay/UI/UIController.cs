using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _winMenu;
    [SerializeField] GameObject _looseMenu;

    public static UIController I { get; private set; }
    
    public Button callWaveButton;

    void Awake()
    {
        I = this;
        if (callWaveButton != null) callWaveButton.onClick.AddListener(OnCallWave);
    }

    void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        //if (Game.I == null) return;
        //moneyText.text = $"$ {Game.I.State.money}";
        //livesText.text = $"❤ {Game.I.State.lives}";

        //var wc = Game.I.State.WaveController;
        //if (wc != null)
        //{
        //    waveStatusText.text = wc.IsWaveActive ? "Wave: ACTIVE" : "Wave: PREP";
        //    nextWaveTimerText.text = wc.IsWaveActive ? "-" : $"{Mathf.CeilToInt(wc.TimeToNextWave)}s";
        //    if (callWaveButton != null) callWaveButton.interactable = !wc.IsWaveActive && wc.TimeToNextWave > 0f;
        //}
    }
    public void ShowWinMenu()
    {
        _winMenu.SetActive(true);
    }
    public void ShowLooseMenu()
    {
        _looseMenu.SetActive(true);
    }
    void OnCallWave()
    {

    }

    public void OnOutsideClick()
    {

    }
}
