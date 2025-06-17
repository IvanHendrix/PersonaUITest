using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class MenuButton : MonoBehaviour
    {
        public event Action OnClick;
        
        [SerializeField] private Button button;
        [SerializeField] private HighlightNotify notify;

        public void Select()
        {
            button.Select();
            notify.Select();
        }
        
        private void Start()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}