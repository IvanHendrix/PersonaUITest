using UnityEngine;

namespace Infrastructure.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [Header("Audio Sources")]
        public AudioSource SfxSource;
        public AudioSource MusicSource;

        [Space]
        public AudioClip Click;
        public AudioClip ButtonSwitch;
        public AudioClip Tab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySFX(SoundType soundType)
        {
            AudioClip audioClip = GetSound(soundType);

            if (audioClip == null)
            {
                return;
            }
            
            SfxSource.PlayOneShot(audioClip);
        }

        public void PlayMusic()
        {
            MusicSource.Play();
        }

        public void StopMusic()
        {
            MusicSource.Stop();
        }

        private AudioClip GetSound(SoundType soundType)
        {
            AudioClip audioClip = null;

            switch (soundType)
            {
                case SoundType.Click:
                    audioClip = Click;
                    break;
                case SoundType.ButtonSwitch:
                    audioClip = ButtonSwitch;
                    break;
                case SoundType.Tab:
                    audioClip = Tab;
                    break;
            }

            return audioClip;
        }
    }
}