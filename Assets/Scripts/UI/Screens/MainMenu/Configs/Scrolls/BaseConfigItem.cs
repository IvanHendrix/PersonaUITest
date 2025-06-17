using UnityEngine;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public abstract class BaseConfigItem : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            RegisterInput();
            EnableInput();
        }

        protected virtual void OnDisable()
        {
            UnregisterInput();
        }

        protected abstract void RegisterInput();
        protected abstract void UnregisterInput();
        protected virtual void EnableInput()
        {
            UIInputHub.Instance.LeftArrow.Enable();
            UIInputHub.Instance.RightArrow.Enable();
            UIInputHub.Instance.Submit.Enable();
        }
    }
}