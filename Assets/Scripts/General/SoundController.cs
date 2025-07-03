using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

namespace General
{
    public class SoundController : MonoBehaviour
    {
        private const string MusicVolumeKey = "MusicVolume";
        private const string SFXVolumeKey = "SFXVolume";
        
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Awake()
        {
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

            musicSlider.minValue = 0.001f;
            sfxSlider.minValue = 0.001f;
        }

        private void OnDisable()
        {
            PlayerPrefs.Save();
        }

        public void SetCurrentVolume()
        {
            float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1);
            float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1);

            musicSlider.SetValueWithoutNotify(musicVolume);
            sfxSlider.SetValueWithoutNotify(sfxVolume);
            
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
        }
        
        private void SetMusicVolume(float value)
        {
            SetVolume(MusicVolumeKey, value);
        }

        private void SetSFXVolume(float value)
        {
            SetVolume(SFXVolumeKey, value);
        }

        private void SetVolume(string bus, float value)
        {
            float volume = Mathf.Log(value) * 20;
            
            audioMixer.SetFloat(bus, volume);
            
            PlayerPrefs.SetFloat(bus, value);
        }
    }
}