using System;
using UnityEngine;

namespace Infrastructure.Hints
{
    [Serializable]
    public class InputHintDefinition
    {
        public string id;

        [Header("Text values per scheme")]
        public string keyboardText;
        public string xboxText;
        public string ps5Text;

        [Header("Sprite names in SpriteAtlas")]
        public Sprite keyboardSpriteName;
        public Sprite xboxSpriteName;
        public Sprite ps5SpriteName;

        public string GetText(string scheme) => scheme switch
        {
            "Keyboard" => keyboardText,
            "Xbox" => xboxText,
            "PS5" => ps5Text,
            _ => keyboardText
        };

        public Sprite GetSpriteName(string scheme) => scheme switch
        {
            "Keyboard" => keyboardSpriteName,
            "Xbox" => xboxSpriteName,
            "PS5" => ps5SpriteName,
            _ => null
        };
    }
}