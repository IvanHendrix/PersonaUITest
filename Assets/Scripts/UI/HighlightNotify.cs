using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HighlightNotify : MonoBehaviour, ISelectHandler
    {
        [SerializeField] private RectTransform visualTarget;
        [SerializeField] private TextMeshProUGUI label;

        public void Select()
        {
            HighlightController.Instance.Highlight(visualTarget, label);
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            HighlightController.Instance.Highlight(visualTarget, label);
        }
    }
}