using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Properties

    public static GameManager Instance
    {
        get => _instance;
    }

    #endregion

    #region Fields

    [SerializeField] private TextMeshProUGUI _numberOfElementalsText;

    private static GameManager _instance;
    private int _numberOfElementals;

    #endregion

    #region Unity Messages

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    #endregion

    #region Public Methods

    public void AddElemental()
    {
        _numberOfElementals++;
        UpdateNumberOfElementalsText();
    }

    public void RemoveElemental()
    {
        _numberOfElementals--;
        UpdateNumberOfElementalsText();

        if (_numberOfElementals <= 0)
        {
            Victory();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    #endregion

    #region Private Methods

    private void UpdateNumberOfElementalsText()
    {
        _numberOfElementalsText.text = _numberOfElementals.ToString();
    }

    private void Victory()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    #endregion
}
