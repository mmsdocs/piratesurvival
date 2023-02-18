using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("TreasureWorldLevel");

    public void Settings() => SceneManager.LoadScene("Settings");

    public void ExitGame() => Application.Quit();
}
