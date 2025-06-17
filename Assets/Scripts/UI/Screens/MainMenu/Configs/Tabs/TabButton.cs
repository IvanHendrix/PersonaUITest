using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Screens.MainMenu.Configs.Tabs
{
    public class TabButton : MonoBehaviour,ISelectHandler,IPointerClickHandler
    {
        public event Action<int> OnSelected;

        public int Index;
        public RectTransform rectTransform;
        public TextMeshProUGUI label;

        public void SetSelected()
        {
            HighlightController.Instance.Highlight(rectTransform, label);
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            SetSelected();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSelected();
            OnSelected?.Invoke(Index);
        }
    }
}