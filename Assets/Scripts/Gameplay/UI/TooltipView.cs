using TMPro;
using UnityEngine;

namespace Inventory.UI
{
    public class TooltipView : MonoBehaviour
    {
        [SerializeField] UIElementFitController _fitter;
        [SerializeField] GameObject _panel;
        [SerializeField] TMP_Text _txComponent;

        [SerializeField] Camera _uiCamera;

        Vector2 _targetScPos;

        private void Start()
        {
            EventBus.Subscribe<TooltipEventArgs>(OnTooltipEvent);
            _panel.SetActive(false);
        }
        void OnTooltipEvent(TooltipEventArgs e)
        {
            _panel.SetActive(e.display);

            if (_panel.activeSelf == false)
                return;

            _txComponent.text = e.text;

            _fitter.SetScreenPosition(e.screenPosition);
        }
    }
}
