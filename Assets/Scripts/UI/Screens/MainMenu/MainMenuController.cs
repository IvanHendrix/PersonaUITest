using System.Collections.Generic;
using Infrastructure.Sounds;
using UI.Screens.MainMenu.Configs;
using UI.Screens.MainMenu.Credits;
using UI.Screens.MainMenu.Progress;
using UI.Screens.MainMenu.StartMenu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private StartMenuView startMenuView;
        [SerializeField] private ConfigMenuView configPanel;
        [SerializeField] private ProgressMenuView progressMenuView;
        [SerializeField] private CreditsMenuView creditsMenuView;

        [SerializeField] private List<Sprite> backgroundSprites;
        [SerializeField] private Image switchedBackground;

        private List<UIMenuView> _views = new List<UIMenuView>();

        private void Start()
        {
            startMenuView.OnConfigClick += ShowConfig;
            startMenuView.OnProgressClick += ShowProgress;
            startMenuView.OnCreditsClick += ShowCredits;
            
            configPanel.OnFallBackEvent += ShowMainMenu;
            progressMenuView.OnFallBackEvent += ShowMainMenu;
            creditsMenuView.OnFallBackEvent += ShowMainMenu;
            
            _views.Add(startMenuView);
            _views.Add(configPanel);
            _views.Add(progressMenuView);
            _views.Add(creditsMenuView);
        }

        private void ShowMainMenu()
        {
            DisableViews();
            
            startMenuView.gameObject.SetActive(true);
            switchedBackground.sprite = backgroundSprites[0];
            startMenuView.Show();
            SoundManager.Instance.PlaySFX(SoundType.Click);
        }

        private void ShowConfig()
        {
            DisableViews();
            
            configPanel.gameObject.SetActive(true);
            configPanel.Show();
            switchedBackground.sprite = backgroundSprites[1];
        }
        
        private void ShowProgress()
        {
            DisableViews();
            
            progressMenuView.gameObject.SetActive(true);
            progressMenuView.Show();
            switchedBackground.sprite = backgroundSprites[2];
        }
        
        private void ShowCredits()
        {
            DisableViews();
            
            creditsMenuView.gameObject.SetActive(true);
            creditsMenuView.Show();
            switchedBackground.sprite = backgroundSprites[3];
        }

        private void DisableViews()
        {
            foreach (var view in _views)
            {
                view.gameObject.SetActive(false);
            }
        }
        
        private void OnDestroy()
        {
            startMenuView.OnConfigClick -= ShowConfig;
            startMenuView.OnProgressClick -= ShowProgress;
            startMenuView.OnCreditsClick -= ShowCredits;
            
            configPanel.OnFallBackEvent -= ShowMainMenu;
            progressMenuView.OnFallBackEvent -= ShowMainMenu;
            creditsMenuView.OnFallBackEvent -= ShowMainMenu;
        }
    }
}