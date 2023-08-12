using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        sfxSource.Play();
    }

    public void GoBackFromSettings()
    {
        settingsPanel.SetActive(false);
        sfxSource.Play();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
