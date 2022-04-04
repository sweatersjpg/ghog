using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;

    int firstRun;

    private void Start()
    {
        firstRun = PlayerPrefs.GetInt("firstTime");
        if (firstRun == 0)
        {
            firstRun = 1;
            PlayerPrefs.SetInt("firstTime", firstRun);
            PlayerPrefs.SetFloat("gameVolume", 1);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
        }
    }

    public void Resume()
    {
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoMenu(int menuIndex)
    {
        SceneManager.LoadScene(menuIndex);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }

}
