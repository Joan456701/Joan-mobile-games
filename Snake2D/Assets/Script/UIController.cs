using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TextMeshProUGUI points;
    public TextMeshProUGUI gameOver;
    public Button restartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        gameOver.enabled = false;
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartScene);
    }

    public void RestartScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayGameOver()
    {
        gameOver.enabled = true;
        restartButton.gameObject.SetActive(true);
    }

    public void DisplayScore(int score)
    {
        points.text = score.ToString();
    }
}
