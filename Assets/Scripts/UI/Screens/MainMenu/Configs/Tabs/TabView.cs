using UI.Screens.MainMenu.Configs.Scrolls;
using UnityEngine;

namespace UI.Screens.MainMenu.Configs.Tabs
{
    public class TabView : MonoBehaviour
    {
        [SerializeField] private FocusScrollController focusScrollController;

        public void Show()
        {
            focusScrollController.Show();
        }

        public void Reset()
        {
            focusScrollController.Reset();
        }
    }
}