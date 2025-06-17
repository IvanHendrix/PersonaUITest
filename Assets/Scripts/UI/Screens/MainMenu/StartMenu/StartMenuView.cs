using System;
using UnityEngine;

namespace UI.Screens.MainMenu.StartMenu
{
    public class StartMenuView : UIMenuView
    {
        public event Action OnConfigClick;
        public event Action OnProgressClick;
        public event Action OnCreditsClick;
        
        [SerializeField] private MenuButton startNewGameButton;
        [SerializeField] private MenuButton configButton;
        [SerializeField] private MenuButton progressButton;
        [SerializeField] private MenuButton creditButton;

        private void OnEnable()
        {
            SendSelectedObject(startNewGameButton.gameObject);
            startNewGameButton.Select();
        }

        public void Show()
        {
            SendSelectedObject(startNewGameButton.gameObject);
            startNewGameButton.Select();
        }
        
        private void Start()
        {
            configButton.OnClick += OnConfigButtonClick;
            progressButton.OnClick += OnProgressButtonClick;
            creditButton.OnClick += OnCreditsButtonClick;
        }

        private void OnConfigButtonClick()
        {
            OnConfigClick?.Invoke();
        }

        private void OnProgressButtonClick()
        {
            OnProgressClick?.Invoke();
        }
        
        private void OnCreditsButtonClick()
        {
            OnCreditsClick?.Invoke();
        }
        
        private void OnDestroy()
        {
            configButton.OnClick -= OnConfigButtonClick;
            progressButton.OnClick -= OnProgressButtonClick;
            creditButton.OnClick -= OnCreditsButtonClick;
        }
    }
}