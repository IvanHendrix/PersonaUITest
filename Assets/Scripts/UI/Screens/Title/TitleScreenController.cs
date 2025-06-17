using System.Collections;
using DG.Tweening;
using Infrastructure.Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.Screens.Title
{
    public class TitleScreenController : MonoBehaviour
    {
        private const string NextSceneName = "MainMenu";
        
        [SerializeField] private CanvasGroup pressAnyKeyGroup;
        [SerializeField] private float fadeSpeed = 2f;
        [SerializeField] private RectTransform highlightFX;
        
        private bool _hasPressed = false;
        
        private Sequence _loopingSequence;
        
        private void Start()
        {
            if (pressAnyKeyGroup != null)
            {
                StartCoroutine(FadeLoop());
            }
            
            SoundManager.Instance.PlayMusic();
            
            _loopingSequence = DOTween.Sequence()
                .Append(highlightFX.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.3f).SetEase(Ease.InOutSine))
                .Append(highlightFX.DOScale(new Vector3(0.8f, 1.2f, 1f), 0.3f).SetEase(Ease.InOutSine))
                .Append(highlightFX.DOScale(Vector3.one, 0.4f).SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Restart);
        }

        private void Update()
        {
            if (_hasPressed)
            {
                return;
            }

            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                TriggerContinue();
                return;
            }

            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                TriggerContinue();
                return;
            }

            if (IsAnyGamepadButtonPressed())
            {
                TriggerContinue();
            }
        }

        private void TriggerContinue()
        {
            _hasPressed = true;

            if (pressAnyKeyGroup != null)
            {
                pressAnyKeyGroup.alpha = 0;
            }

            LoadNextScene();
        }

        private bool IsAnyGamepadButtonPressed()
        {
            foreach (var gamepad in Gamepad.all)
            {
                if (
                    gamepad.buttonSouth.wasPressedThisFrame ||
                    gamepad.buttonNorth.wasPressedThisFrame ||
                    gamepad.buttonWest.wasPressedThisFrame ||
                    gamepad.buttonEast.wasPressedThisFrame ||
                    gamepad.startButton.wasPressedThisFrame ||
                    gamepad.selectButton.wasPressedThisFrame ||
                    gamepad.leftShoulder.wasPressedThisFrame ||
                    gamepad.rightShoulder.wasPressedThisFrame ||
                    gamepad.leftStickButton.wasPressedThisFrame ||
                    gamepad.rightStickButton.wasPressedThisFrame ||
                    gamepad.dpad.up.wasPressedThisFrame ||
                    gamepad.dpad.down.wasPressedThisFrame ||
                    gamepad.dpad.left.wasPressedThisFrame ||
                    gamepad.dpad.right.wasPressedThisFrame
                )
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadNextScene()
        {
            _loopingSequence?.Kill();
            SoundManager.Instance.PlaySFX(SoundType.Click);
            SceneManager.LoadScene(NextSceneName);
        }

        private IEnumerator FadeLoop()
        {
            float t = 0f;
            bool fadingIn = true;

            while (!_hasPressed)
            {
                t += Time.deltaTime * fadeSpeed * (fadingIn ? 1 : -1);
                t = Mathf.Clamp01(t);
                pressAnyKeyGroup.alpha = t;

                if (t == 1f || t == 0f)
                {
                    fadingIn = !fadingIn;
                }

                yield return null;
            }
        }
    }
}