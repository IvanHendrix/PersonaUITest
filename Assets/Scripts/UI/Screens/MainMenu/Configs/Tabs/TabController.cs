using System;
using System.Collections.Generic;
using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens.MainMenu.Configs.Tabs
{
    public class TabController : MonoBehaviour
    {
        public event Action OnFallBackClick;
        
        [Space]
        [SerializeField] private List<TabButton> tabButtons;
        [SerializeField] private List<TabView> tabsPanel;

        private int _currentIndex = -1;

        private void OnEnable()
        {
            UIInputHub.Instance.TabLeft.performed += OnTabLeft;
            UIInputHub.Instance.TabLeft.Enable();

            UIInputHub.Instance.TabRight.performed += OnTabRight;
            UIInputHub.Instance.TabRight.Enable();

            UIInputHub.Instance.Back.performed += OnBack;
            UIInputHub.Instance.Back.Enable();
        }

        private void Start()
        {
            foreach (var button in tabButtons)
            {
                button.OnSelected += SelectTabByButton;
            }
            
            NavigateRight();
        }

        private void OnTabLeft(InputAction.CallbackContext ctx)
        {
            NavigateLeft();
        }

        private void OnTabRight(InputAction.CallbackContext ctx)
        {
            NavigateRight();
        }

        private void OnBack(InputAction.CallbackContext ctx)
        {
            OnFallBackClick?.Invoke();
        }

        private void NavigateLeft()
        {
            _currentIndex = (_currentIndex - 1 + tabButtons.Count) % tabButtons.Count;
            Select();
        }

        private void NavigateRight()
        {
            _currentIndex = (_currentIndex + 1) % tabButtons.Count;
            Select();
        }

        private void Select()
        {
            SelectTab(_currentIndex);
            SelectButton(_currentIndex);
            SoundManager.Instance.PlaySFX(SoundType.Tab);
        }

        public void SelectTab(int index)
        {
            foreach (var tab in tabsPanel)
            {
                tab.gameObject.SetActive(false);
                tab.Reset();
            }

            tabsPanel[index].gameObject.SetActive(true);
            tabsPanel[index].Show();
        }

        public void SelectButton(int index)
        {
            tabButtons[index].SetSelected();
        }

        private void SelectTabByButton(int index)
        {
            _currentIndex = index;
            SelectTab(_currentIndex);
        }
        
        private void OnDisable()
        {
            UIInputHub.Instance.TabLeft.performed -= OnTabLeft;
            UIInputHub.Instance.TabRight.performed -= OnTabRight;
            UIInputHub.Instance.Back.performed -= OnBack;
        }

        private void OnDestroy()
        {
            foreach (var button in tabButtons)
            {
                button.OnSelected -= SelectTabByButton;
            }
        }
    }
}