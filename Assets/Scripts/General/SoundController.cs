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

        private float MusicVolume => PlayerPrefs.GetFloat(MusicVolumeKey, 1);
        private float SfxVolume => PlayerPrefs.GetFloat(SFXVolumeKey, 1);

        private void Awake()
        {
            if (!musicSlider || !sfxSlider)
                return;

            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);

            musicSlider.minValue = 0.001f;
            sfxSlider.minValue = 0.001f;

            UpdateSlidersToCurrentVolume();
        }

        private void OnDisable()
        {
            PlayerPrefs.Save();
        }

        private void UpdateSlidersToCurrentVolume()
        {
            musicSlider?.SetValueWithoutNotify(MusicVolume);
            sfxSlider?.SetValueWithoutNotify(SfxVolume);
        }

        public void SetCurrentVolume()
        {
            SetMusicVolume(MusicVolume);
            SetSFXVolume(SfxVolume);
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