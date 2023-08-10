using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    #endregion

    #region Unity Messages

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(LoadMainMenu);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    #endregion

    #region Private Methods

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
