using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class ConfigToggle : BaseConfigItem
    {
        [SerializeField] private Toggle toggle;

        protected override void RegisterInput()
        {
            UIInputHub.Instance.Submit.performed += OnToggle;
        }

        protected override void UnregisterInput()
        {
            UIInputHub.Instance.Submit.performed -= OnToggle;
        }

        private void OnToggle(InputAction.CallbackContext ctx)
        {
            if (!IsSelected())
            {
                return;
            }
            
            toggle.isOn = !toggle.isOn;
            
            SoundManager.Instance.PlaySFX(SoundType.Click);
        }

        private bool IsSelected() => EventSystem.current.currentSelectedGameObject == gameObject;
    }
}