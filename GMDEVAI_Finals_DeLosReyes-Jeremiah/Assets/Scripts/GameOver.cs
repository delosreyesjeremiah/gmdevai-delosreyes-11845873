using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    #region Fields

    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _quitButton;

    #endregion

    #region Unity Messages

    private void OnEnable()
    {
        _retryButton.onClick.AddListener(RestartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _retryButton.onClick.RemoveListener(RestartGame);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    #endregion

    #region Private Methods

    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
