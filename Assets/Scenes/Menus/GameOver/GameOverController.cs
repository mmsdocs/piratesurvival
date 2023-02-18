using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;
    public Image background;
    public AudioSource timeoutSfx;
    public AudioSource killedSfx;

    private void Start()
    {
        bool wasTimeout = PlayerPrefs.GetString("wasTimeout", "false").Equals("true");
        
        background.color = wasTimeout ? Color.white : Color.red;

        if (wasTimeout) timeoutSfx.Play();
        else killedSfx.Play();

        int score = PlayerPrefs.GetInt("lastScore", 0);
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        
        SetScore(score);
        SetHighscore(highscore);
    }

    private void SetScore(int value)
    {
        score.text = value.ToString();
    }

    private void SetHighscore(int value) 
    {
        highscore.text = value.ToString();
    }

    public void RestartGame() => SceneManager.LoadScene("TreasureWorldLevel");

    public void MainMenu() => SceneManager.LoadScene("MainMenu");
}
