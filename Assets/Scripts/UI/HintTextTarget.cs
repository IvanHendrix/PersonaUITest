using TMPro;
using UnityEngine;

namespace UI
{
    public class HintTextTarget : MonoBehaviour, IInputHintTarget
    {
        [SerializeField] private string hintKey;
        [SerializeField] private TextMeshProUGUI target;

        public string HintKey => hintKey;

        private void Start()
        {
            InputHintPresenter.Register(this);
        }

        public void Apply(string text, Sprite icon)
        {
            if (target != null)
            {
                target.text = text;
            }
        }

        private void OnDestroy()
        {
            InputHintPresenter.Unregister(this);
        }
    }
}