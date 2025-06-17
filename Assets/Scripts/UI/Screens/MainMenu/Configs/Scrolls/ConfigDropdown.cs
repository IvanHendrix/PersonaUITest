using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class ConfigDropdown : BaseConfigItem
    {
        [SerializeField] private TMP_Dropdown dropdown;

        protected override void RegisterInput()
        {
            UIInputHub.Instance.Back.performed += OnBack;
        }

        protected override void UnregisterInput()
        {
            UIInputHub.Instance.Back.performed -= OnBack;
        }

        private void OnBack(InputAction.CallbackContext ctx)
        {
            dropdown.Show();
        }
    }
}