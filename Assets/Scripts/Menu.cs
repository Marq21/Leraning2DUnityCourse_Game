using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text playerScore;
    public Slider musicSlider, soundSlider;
    public Text musicText, soundText;
    public AudioSource musicSource, soundSource;

    void Start()
    {
        if (PlayerPrefs.HasKey("playerScore"))
        {
            playerScore.text = "SCORE: " + PlayerPrefs.GetInt("playerScore").ToString();
        }
        else
        {
            playerScore.text = "SCORE: 0";
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetInt("musicVolume", 3);
        if (!PlayerPrefs.HasKey("soundVolume"))
            PlayerPrefs.SetInt("soundVolume", 6);

        musicSlider.value = PlayerPrefs.GetInt("musicVolume");
        soundSlider.value = PlayerPrefs.GetInt("soundVolume");

        musicSource.volume = PlayerPrefs.GetInt("musicVolume") * 0.1f;
        soundSource.volume = PlayerPrefs.GetInt("soundVolume") * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("musicVolume", (int)musicSlider.value);
        PlayerPrefs.SetInt("soundVolume", (int)soundSlider.value);
        musicText.text = musicSlider.value.ToString();
        soundText.text = soundSlider.value.ToString();
        musicSource.volume = PlayerPrefs.GetInt("musicVolume") * 0.1f;
        soundSource.volume = PlayerPrefs.GetInt("soundVolume") * 0.1f;
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ClearSaves()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
