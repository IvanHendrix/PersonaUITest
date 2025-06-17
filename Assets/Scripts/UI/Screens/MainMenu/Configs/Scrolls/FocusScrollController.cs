using System.Collections.Generic;
using DG.Tweening;
using Input;
using UnityEngine;

namespace UI.Screens.MainMenu.Configs.Scrolls
{
    public class FocusScrollController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float scrollDuration = 0.2f;

        [Space]
        [SerializeField] private RectTransform viewport;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject defaultFocusElement;

        [SerializeField] private List<FocusAware> focusAwares;

        private Tween _currentTween;

        private void OnEnable()
        {
            ResetScroll();
        }

        private void Start()
        {
            foreach (var focusAware in focusAwares)
            {
                focusAware.OnSelected += ScrollToEnsureVisible;
            }
        }

        public void Show()
        {
            Invoke(nameof(SetInitialFocus), 0.01f);
        }

        public void Reset()
        {
            ResetScroll();
        }

        private void SetInitialFocus()
        {
            UIControlModeSwitcher.Instance.SetDefaultSelectedObject(defaultFocusElement);
            focusAwares[0].Show();
        }

        private void ResetScroll()
        {
            _currentTween?.Kill();
            content.anchoredPosition = Vector2.zero;
        }

        private void ScrollToEnsureVisible(RectTransform target)
        {
            if (target == null)
            {
                return;
            }

            Bounds itemBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewport, target);

            float padding = 5f;
            float scrollOffset = 0f;

            if (itemBounds.min.y < viewport.rect.yMin + padding)
            {
                scrollOffset = viewport.rect.yMin + padding - itemBounds.min.y;
            }
            else if (itemBounds.max.y > viewport.rect.yMax - padding)
            {
                scrollOffset = viewport.rect.yMax - padding - itemBounds.max.y;
            }

            if (Mathf.Abs(scrollOffset) > 0.01f)
            {
                Vector2 current = content.anchoredPosition;
                Vector2 targetPos = current + new Vector2(0f, scrollOffset);

                float maxScroll = content.rect.height - viewport.rect.height;
                targetPos.y = Mathf.Clamp(targetPos.y, 0, maxScroll);

                _currentTween?.Kill();
                _currentTween = content
                    .DOAnchorPos(targetPos, scrollDuration)
                    .SetEase(Ease.OutCubic)
                    .SetUpdate(true);
            }
        }

        private void OnDestroy()
        {
            foreach (var focusAware in focusAwares)
            {
                focusAware.OnSelected -= ScrollToEnsureVisible;
            }
        }
    }
}