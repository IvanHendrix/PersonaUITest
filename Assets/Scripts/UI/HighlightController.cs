using DG.Tweening;
using Infrastructure.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HighlightController : MonoBehaviour
    {
        public static HighlightController Instance { get; private set; }

        [Header("References")] 
        [SerializeField] private RectTransform highlightFX;

        [SerializeField] private Image highlightImage;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Sprite[] possibleBackgrounds;

        [Header("Animation")]
        [SerializeField] private float moveDuration = 0.15f;
        [SerializeField] private Vector3 scaleUp = new Vector3(1.1f, 1.1f, 1f);
        [SerializeField] private float fadeDuration = 0.2f;

        [Header("Colors")]
        [SerializeField] private Color defaultTextColor;
        [SerializeField] private Color selectedTextColor;

        private Tween _currentTween;
        private TextMeshProUGUI _lastHighlightedLabel;
        private Sequence _loopingSequence;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            highlightFX.gameObject.SetActive(false);
        }

        public void Show(bool show)
        {
            if (!show)
            {
                _currentTween?.Kill();
                _loopingSequence?.Kill();
            }
     
            highlightFX.gameObject.SetActive(show);
        }
        
        public void Highlight(RectTransform target, TextMeshProUGUI label)
        {
            if (target == null)
            {
                return;
            }
            
            if (_lastHighlightedLabel != null)
            {
                _lastHighlightedLabel.color = defaultTextColor;
            }
            
            if (highlightImage != null && possibleBackgrounds.Length > 0)
            {
                var sprite = possibleBackgrounds[Random.Range(0, possibleBackgrounds.Length)];
                highlightImage.sprite = sprite;
            }

            if (label != null)
            {
                label.color = selectedTextColor;
                _lastHighlightedLabel = label;
            }
            
            Vector2 localPos = highlightFX.parent.InverseTransformPoint(target.position);

            highlightFX.gameObject.SetActive(true);
            highlightFX.localRotation = Quaternion.identity;

            _currentTween?.Kill();
            _loopingSequence?.Kill();

            highlightFX.localScale = Vector3.one * 0.85f;

            _currentTween = highlightFX
                .DOAnchorPos(localPos, moveDuration)
                .SetEase(Ease.OutBack);

            highlightFX.DOScale(scaleUp, moveDuration).SetEase(Ease.OutExpo);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1f, fadeDuration);
            }

            _loopingSequence = DOTween.Sequence()
                .Append(highlightFX.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.3f).SetEase(Ease.InOutSine))
                .Append(highlightFX.DOScale(new Vector3(0.8f, 1.2f, 1f), 0.3f).SetEase(Ease.InOutSine))
                .Append(highlightFX.DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Restart);
            
            SoundManager.Instance.PlaySFX(SoundType.ButtonSwitch);
        }
    }
}