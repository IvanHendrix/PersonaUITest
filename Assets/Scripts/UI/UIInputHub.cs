using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIInputHub : MonoBehaviour
    {
        public static UIInputHub Instance { get; private set; }

        [Header("Tab Navigation (RB/LB or Q/E)")]
        [SerializeField] private InputActionReference tabLeft;
        [SerializeField] private InputActionReference tabRight;

        [Header("UI Control (Back, Arrows)")]
        [SerializeField] private InputActionReference back;
        [SerializeField] private InputActionReference leftArrow;
        [SerializeField] private InputActionReference rightArrow;
        [SerializeField] private InputActionReference submit;

        public InputAction TabLeft => tabLeft.action;
        public InputAction Submit => submit.action;
        public InputAction TabRight => tabRight.action;
        public InputAction Back => back.action;
        public InputAction LeftArrow => leftArrow.action;
        public InputAction RightArrow => rightArrow.action;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnEnable()
        {
            EnableAll();
        }

        private void OnDisable()
        {
            DisableAll();
        }

        private void EnableAll()
        {
            leftArrow.action.Enable();
            tabRight.action.Enable();
            back.action.Enable();
            leftArrow.action.Enable();
            rightArrow.action.Enable();
            submit.action.Enable();
        }

        private void DisableAll()
        {
            leftArrow.action.Disable();
            tabRight.action.Disable();
            back.action.Disable();
            leftArrow.action.Disable();
            rightArrow.action.Disable();
            submit.action.Disable();
        }
    }
}