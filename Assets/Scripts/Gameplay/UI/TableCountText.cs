using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class TableCountText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    void Update()
    {
        _text.text = $"Золота в куче:<color=yellow> {ActionManager.I.gameState.money}/{ActionManager.I.settings.maxPileCountForVisual}";
    } 
    
}
