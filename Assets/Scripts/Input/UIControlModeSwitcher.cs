using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class UIControlModeSwitcher : MonoBehaviour
    {
        public static UIControlModeSwitcher Instance { get; private set; }
        
        [SerializeField] private GameObject defaultSelectedObject;

        private string _lastScheme = "";
        private bool _usingGamepad;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            if (PlayerInput.all.Count > 0)
            {
                PlayerInput playerInput = PlayerInput.all[0];
                playerInput.onControlsChanged += OnControlsChanged;
                OnControlsChanged(playerInput);
            }
        }

        public void SetDefaultSelectedObject(GameObject defaultSelected)
        {
            defaultSelectedObject = defaultSelected;
        }

        private void OnControlsChanged(PlayerInput input)
        {
            string scheme = input.currentControlScheme;

            if (scheme == _lastScheme)
            {
                return;
            }
            
            _lastScheme = scheme;

            if (scheme == "Gamepad")
            {
                EnableGamepadMode();
            }
            else
            {
                EnableMouseMode();
            }
        }

        private void EnableGamepadMode()
        {
            _usingGamepad = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (defaultSelectedObject != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
            }
        }

        private void EnableMouseMode()
        {
            _usingGamepad = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            EventSystem.current.SetSelectedGameObject(null); 
        }
        
        private void OnDestroy()
        {
            if (PlayerInput.all.Count > 0)
            {
                PlayerInput.all[0].onControlsChanged -= OnControlsChanged;
            }
        }
    }
}