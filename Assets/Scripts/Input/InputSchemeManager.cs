using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputSchemeManager : MonoBehaviour
    {
        public event Action<string> OnSchemeChanged;
        
        public static InputSchemeManager Instance { get; private set; }
        public string CurrentScheme { get; private set; } = "Keyboard";
        
        private string _lastScheme;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InputSystem.onDeviceChange += OnDeviceChange;
            DetectAndBroadcast();
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            DetectAndBroadcast();
        }

        private void DetectAndBroadcast()
        {
            string scheme = DetectScheme();

            if (scheme != _lastScheme)
            {
                _lastScheme = scheme;
                CurrentScheme = scheme;
                
                Debug.Log(scheme);
                
                OnSchemeChanged?.Invoke(scheme);
            }
        }

        private string DetectScheme()
        {
            var gamepad = Gamepad.current;

            if (gamepad != null)
            {
                string layout = gamepad.layout ?? "";
                string name = gamepad.name ?? "";
                string displayName = gamepad.displayName ?? "";

                if (layout.ToLower().Contains("xinput") || name.ToLower().Contains("xbox"))
                    return "Xbox";

                if (layout.ToLower().Contains("dualsense") || name.ToLower().Contains("sony") || displayName.ToLower().Contains("wireless"))
                    return "PS5";
            }

            return "Keyboard";
        }

        private void OnDestroy()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }
    }
}