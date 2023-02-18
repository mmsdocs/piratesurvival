using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private MainMenuMusicController music;
    private void Start()
    {
        music = MainMenuMusicController.Instance;

        if (!music.IsPlaying()) music.Play();
    }

    public void StartGame()
    {
        music.Stop();
        SceneManager.LoadScene("TreasureWorldLevel");
    }

    public void Settings() => SceneManager.LoadScene("Settings");

    public void ExitGame() => Application.Quit();
}
