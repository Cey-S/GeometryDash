using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioSource soundSource;

    private Slider volumeSlider;
    private float volume;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();

        if (gameObject.name.Contains("Music"))
        {
            volume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        }
        else if (gameObject.name.Contains("SFX"))
        {
            volume = PlayerPrefs.GetFloat("sfxVolume", 1.0f);
        }

        soundSource.volume = volume;
        volumeSlider.value = volume;
    }

    public void AdjustVolume()
    {
        soundSource.volume = volumeSlider.value;
        
        if (gameObject.name.Contains("Music"))
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }
        else if (gameObject.name.Contains("SFX"))
        {
            PlayerPrefs.SetFloat("sfxVolume", volumeSlider.value);
        }
    }


}
