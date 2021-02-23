using UnityEngine;
using System.Collections;

namespace RpgAdventure
{
    public class RandomAudioPlayer : MonoBehaviour
    {
        [System.Serializable]
        public class SoundBank
        {
            public string name;
            public AudioClip[] clips;
        }

        public SoundBank soundBank = new SoundBank();
        private AudioSource m_AudioSource;

        private void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
    }
}
