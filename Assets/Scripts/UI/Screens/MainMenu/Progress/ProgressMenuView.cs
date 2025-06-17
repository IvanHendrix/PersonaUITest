using System;
using Infrastructure.Sounds;
using UI.Screens.MainMenu.Configs.Scrolls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens.MainMenu.Progress
{
    public class ProgressMenuView : UIMenuView
    {
        public event Action OnFallBackEvent;

        [SerializeField] private FocusScrollController focusScrollController;
        
        private void OnEnable()
        {
            UIInputHub.Instance.Back.performed += OnFallBack;
            UIInputHub.Instance.Back.Enable();
            
            SendSelectedObject(null);
            HighlightController.Instance.Show(false);
        }

        public void Show()
        {
            focusScrollController.Show();
            SendSelectedObject(null);
        }

        public void Reset()
        {
            focusScrollController.Reset();
        }

        private void OnFallBack(InputAction.CallbackContext ctx)
        {
            SoundManager.Instance.PlaySFX(SoundType.Click);
            OnFallBackEvent?.Invoke();
        }

        private void OnDisable()
        {
            UIInputHub.Instance.Back.performed -= OnFallBack;
            HighlightController.Instance.Show(true);
        }
    }
}