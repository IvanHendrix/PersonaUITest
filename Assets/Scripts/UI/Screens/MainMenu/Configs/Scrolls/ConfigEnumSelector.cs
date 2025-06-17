using Infrastructure.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class ConfigEnumSelector : BaseConfigItem
    {
        [SerializeField] private TextMeshProUGUI valueLabel;
        [SerializeField] private string[] options;
        
        private int _currentIndex;

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
            
            _currentIndex = (_currentIndex - 1 + options.Length) % options.Length;
            UpdateLabel();
            
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }

        private void OnRight(InputAction.CallbackContext ctx)
        {
            if (!IsSelected())
            {
                return;
            }
            
            _currentIndex = (_currentIndex + 1) % options.Length;
            UpdateLabel();
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }

        private void UpdateLabel() => valueLabel.text = options[_currentIndex];
        private bool IsSelected() => EventSystem.current.currentSelectedGameObject == gameObject;
    }
}