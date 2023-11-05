using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsMenuPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenOptions()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void ExitOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        //Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Compilado
        //Application.Quit();
    }
}
