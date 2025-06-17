using UnityEngine;

namespace UI
{
    public interface IInputHintTarget
    {
        string HintKey { get; }
        void Apply(string text, Sprite icon);
    }
}