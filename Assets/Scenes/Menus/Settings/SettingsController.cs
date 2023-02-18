using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Slider enemySpawnTimeSlider;
    public TextMeshProUGUI enemySpawnTimeLabel;
    public Slider levelTimeSlider;
    public TextMeshProUGUI levelTimeLabel;

    private void Start()
    {
        enemySpawnTimeSlider.value = PlayerPrefs.GetFloat("enemySpawnTime", 1.0f);
        UpdateEnemySpawnTimeText(enemySpawnTimeSlider.value);

        levelTimeSlider.value = PlayerPrefs.GetInt("levelTime", 60) / 60;
        UpdateLevelTimeText(levelTimeSlider.value);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateEnemySpawnTimeText(float value)
    {
        enemySpawnTimeLabel.text = Utils.Round(value, 2).ToString() + ((value > 1) ? " segundos" : " segundo");
        PlayerPrefs.SetFloat("enemySpawnTime", value);
    }

    public void UpdateLevelTimeText(float value)
    {
        levelTimeLabel.text = value.ToString() + ((value > 1) ? " minutos" : " minuto");
        PlayerPrefs.SetInt("levelTime", (int) value * 60);
    }
        
}
