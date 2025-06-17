using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Hints
{
    public class InputHintBootstrap : MonoBehaviour
    {
        [SerializeField] private InputHintLibrary library;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            InputHintPresenter.Initialize(library);
        }
    }
}