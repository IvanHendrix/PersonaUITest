using System;
using DG.Tweening;
using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens.MainMenu.Credits
{
    public class CreditsMenuView : UIMenuView
    {
        public event Action OnFallBackEvent;
        
        [SerializeField] private RectTransform textTransform;
        [SerializeField] private float scrollDistance = 1000f;
        [SerializeField] private float scrollDuration = 20f;
        [SerializeField] private bool loop = false;
        
        private void OnEnable()
        {
            UIInputHub.Instance.Back.performed += OnFallBack;
            UIInputHub.Instance.Back.Enable();
            
            SendSelectedObject(null);
            HighlightController.Instance.Show(false);
        }

        public void Show()
        {
            SendSelectedObject(null);
            
            Vector2 startPos = new Vector2(0, -scrollDistance);
            Vector2 endPos = new Vector2(0, scrollDistance);

            textTransform.anchoredPosition = startPos;

            textTransform.DOAnchorPos(endPos, scrollDuration)
                .SetEase(Ease.Linear)
                .SetLoops(loop ? -1 : 0)
                .SetUpdate(true);
        }

        private void OnFallBack(InputAction.CallbackContext ctx)
        {
            if (textTransform != null)
            {
                DOTween.Kill(textTransform);
            }
            
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