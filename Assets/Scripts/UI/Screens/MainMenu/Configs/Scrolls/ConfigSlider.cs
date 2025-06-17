using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class ConfigSlider : BaseConfigItem
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float step = 1f;

        private bool IsSelected() => EventSystem.current.currentSelectedGameObject == gameObject;

        protected override void RegisterInput()
        {
            UIInputHub.Instance.LeftArrow.performed += OnLeft;
            UIInputHub.Instance.RightArrow.performed += OnRight;
        }

        protected override void UnregisterInput()
        {
            UIInputHub.Instance.LeftArrow.performed -= OnLeft;
            UIInputHub.Instance.RightArrow.performed -= OnRight;
        }

        private void OnLeft(InputAction.CallbackContext ctx)
        {
            if (!IsSelected())
            {
                return;
            }
            
            slider.value = Mathf.Max(slider.minValue, slider.value - step);
            Refresh();
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }

        private void OnRight(InputAction.CallbackContext ctx)
        {
            if (!IsSelected())
            {
                return;
            }
            
            slider.value = Mathf.Min(slider.maxValue, slider.value + step);
            Refresh();
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }

        private void Refresh()
        {
        }
    }
}