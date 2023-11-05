using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuPanel;

    public void OpenOptions()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void ExitOptions()
    {
        optionsMenuPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Compilado
        //Application.Quit();
    }
}