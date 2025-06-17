using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HintImageTarget : MonoBehaviour, IInputHintTarget
    {
        [SerializeField] private string hintKey;
        [SerializeField] private Image target;

        private void Start()
        {
            InputHintPresenter.Register(this);
        }

        public string HintKey => hintKey;

        public void Apply(string text, Sprite icon)
        {
            if (target != null)
            {
                target.sprite = icon;
                target.enabled = icon != null;
            }
        }

        private void OnDestroy()
        {
            InputHintPresenter.Unregister(this);
        }
    }
}