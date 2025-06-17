using System;
using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class FocusAware : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public event Action<RectTransform> OnSelected;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightColor = Color.cyan;

        public void Show()
        {
            button.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (background != null)
            {
                background.color = highlightColor;
            }

            OnSelected?.Invoke(rectTransform);
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (background != null)
            {
                background.color = normalColor;
            }
        }
    }
}