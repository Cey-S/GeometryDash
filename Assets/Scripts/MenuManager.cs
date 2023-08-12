using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        SettingsPanel.SetActive(true);
    }

    public void GoBackFromSettings()
    {
        SettingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
