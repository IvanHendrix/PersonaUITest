using System;
using Infrastructure.Sounds;
using UI.Screens.MainMenu.Configs.Tabs;
using UnityEngine;

namespace UI.Screens.MainMenu.Configs
{
    public class ConfigMenuView : UIMenuView
    {
        public event Action OnFallBackEvent; 

        [SerializeField] private TabController configTabController;

        private void Start()
        {
            configTabController.OnFallBackClick += OnFallBack;
        }

        public void Show()
        {
            configTabController.SelectTab(0);
            configTabController.SelectButton(0);
        }

        private void OnFallBack()
        {
            SoundManager.Instance.PlaySFX(SoundType.Click);
            OnFallBackEvent?.Invoke();
        }

        private void OnDestroy()
        {
            configTabController.OnFallBackClick += OnFallBack;
        }
    }
}