using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text playerScore;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public AudioSource musicSource, soundSource;
    [SerializeField] private SoundEffector effector;

    public void GameOver()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        gameOverScreen.SetActive(true);
        effector.playLoseSound();

        //Saving Player Score after death
        if (PlayerPrefs.HasKey("playerScore"))
        {
            int lastScore = PlayerPrefs.GetInt("playerScore");
            int currentScore = player.getPlayerScore();
            PlayerPrefs.SetInt("playerScore", player.getPlayerScore());
        }
        else
        {
            PlayerPrefs.SetInt("playerScore", player.getPlayerScore());
        }

    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Start()
    {
        musicSource.volume = PlayerPrefs.GetInt("musicVolume") * 0.1f;
        soundSource.volume = PlayerPrefs.GetInt("soundVolume") * 0.1f;
        Enemy.setDestroytVolumeLevel(PlayerPrefs.GetInt("soundVolume") * 0.1f);
        Time.timeScale = 1f;
        player.enabled = true;
        player.setPlayerScore(0);
    }

    public void Update()
    {
        StringBuilder sb = new StringBuilder("SCORE");
        playerScore.text = sb.Clear().Append("SCORE: ").Append(player.getPlayerScore().ToString()).ToString();
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        pauseScreen.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        pauseScreen.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
