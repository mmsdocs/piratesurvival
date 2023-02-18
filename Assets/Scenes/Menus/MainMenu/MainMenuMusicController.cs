using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicController : MonoBehaviour
{
    public static MainMenuMusicController Instance = null;

    private AudioSource music;

    private void Awake()
    {
        music = GetComponent<AudioSource>();

        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public void Play() => music.Play();
    public void Stop() => music.Stop();
    
    public bool IsPlaying() => music.isPlaying;
}
