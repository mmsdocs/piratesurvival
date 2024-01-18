using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private HUDController hud;
    [SerializeField] private int duration = 60;
    private int score = 0;
    private float currentTime = 0f;

    public static LevelManager Instance;

    private void Awake() => Instance = this;

    private void Start()
    {
        duration = PlayerPrefs.GetInt("levelTime", 60);
        currentTime = duration;

        hud.SetLevelTimeValue((int)currentTime);
    }

    private void Update()
    {
        currentTime = duration - Time.timeSinceLevelLoad;
        if (currentTime > 0f && hud != null) hud.SetLevelTimeValue((int)currentTime, currentTime <= 5f);
        else GameOver();
    }

    public void GameOver()
    {
        int highscore = PlayerPrefs.GetInt("highscore");
        if (score > highscore) PlayerPrefs.SetInt("highscore", score);

        PlayerPrefs.SetInt("lastScore", score);
        PlayerPrefs.SetString("wasTimeout", (currentTime <= 0).ToString());

        SceneManager.LoadScene("GameOver");
    }

    public void AddPoint()
    {
        if (hud == null) return;

        score++;
        hud.SetScoreValue(score);
    }
}
