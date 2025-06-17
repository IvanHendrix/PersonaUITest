using System.Collections.Generic;
using Infrastructure.Hints;
using Input;
using StaticData;
using UnityEngine;

namespace UI
{
    public static class InputHintPresenter
    {
        private const string KeyboardSchemeName = "Keyboard";
        private static readonly List<IInputHintTarget> Targets = new();

        private static InputHintLibrary _hintLibrary;

        private static bool _initialized = false;

        public static void Initialize(InputHintLibrary library)
        {
            if (_initialized)
            {
                return;
            }

            _hintLibrary = library;
            _initialized = true;

            if (InputSchemeManager.Instance != null)
            {
                InputSchemeManager.Instance.OnSchemeChanged += UpdateAll;
                UpdateAll(InputSchemeManager.Instance.CurrentScheme);
            }
        }

        public static void Register(IInputHintTarget target)
        {
            if (!Targets.Contains(target))
            {
                Targets.Add(target);
            }

            string scheme = InputSchemeManager.Instance.CurrentScheme ?? KeyboardSchemeName;
            ApplyToTarget(target, scheme);
        }

        public static void Unregister(IInputHintTarget target)
        {
            Targets.Remove(target);
        }

        private static void UpdateAll(string scheme)
        {
            foreach (IInputHintTarget target in Targets)
            {
                ApplyToTarget(target, scheme);
            }
        }

        private static void ApplyToTarget(IInputHintTarget target, string scheme)
        {
            InputHintDefinition hint = _hintLibrary.GetHint(target.HintKey);

            if (hint == null)
            {
                return;
            }

            string text = hint.GetText(scheme);
            Sprite icon = hint.GetSpriteName(scheme);

            target.Apply(text, icon);
        }
    }
}