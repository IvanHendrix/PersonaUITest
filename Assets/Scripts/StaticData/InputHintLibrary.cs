using System.Collections.Generic;
using Infrastructure.Hints;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "InputHintLibrary", menuName = "UI/Input Hint Library")]
    public class InputHintLibrary : ScriptableObject
    {
        public List<InputHintDefinition> hintDefinitions = new();

        public InputHintDefinition GetHint(string id)
        {
            return hintDefinitions.Find(h => h.id == id);
        }
    }
}