using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    #endregion

    #region Unity Messages

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayGame);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    #endregion

    #region Private Methods

    private void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
